using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using AnApiOfIceAndFire.Data.Books;
using Dapper;
using Newtonsoft.Json;

namespace AnApiOfIceAndFire.DataFeeder
{
    //todo: Make this whole thing prettier later on
    class Program
    {
        private static readonly string[] InitScripts = {
            "create_books_table.sql", "create_characters_table.sql", "create_houses_table.sql",
            "create_book_character_link_table.sql", "create_character_house_link.sql", "create_house_cadetbranch_link.sql"
        };
        
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Please specify the folder where all scripts and data resides!");
            }

            var folder = args[0] + @"\";
            var initialConnectionString = args[1];
            var connectionString = args[2];
            Console.WriteLine($"Looking for needed files in {folder}");

            var books = GetDtoData<BookDto>(folder, "books.json");
            Console.WriteLine($"Parsed {books.Count} number of books");

            var characters = GetDtoData<CharacterDto>(folder, "characters.json");
            Console.WriteLine($"Parsed {characters.Count} number of characters");

            var houses = GetDtoData<HouseDto>(folder, "houses.json");
            Console.WriteLine($"Parsed {houses.Count} number of houses");

            using (var connection = new SqlConnection(initialConnectionString))
            {
                var createDbScript = GetScriptData(folder, "create_database.sql");
                int result = connection.Execute(createDbScript);
                Console.WriteLine($"Created database: {result}");
            }

            using (var connection = new SqlConnection(connectionString))
            {
                foreach (var initScript in InitScripts)
                {
                    var scriptData = GetScriptData(folder, initScript);
                    connection.Execute(scriptData);

                }
                Console.WriteLine("Finished creating tables");

                foreach (var book in books)
                {
                    var mediaType = MediaTypeParser.ParseMediaType(book.MediaType);
                    connection.Execute("INSERT INTO dbo.books VALUES(@Id, @Name, @ISBN, @Authors, @NumberOfPages, @Publisher, @MediaType, @Country, @ReleaseDate)",
                        new
                        {
                            book.Id,
                            book.Name,
                            book.ISBN,
                            Authors = string.Join(";", book.Authors),
                            book.NumberOfPages,
                            book.Publisher,
                            MediaType = mediaType,
                            book.Country,
                            book.ReleaseDate
                        });
                }
                Console.WriteLine("Finished inserting book data");

                foreach (var character in characters)
                {
                    connection.Execute(
                        "INSERT INTO dbo.characters VALUES(@Id, @Name, @Culture, @Born, @Died, @IsFemale, @Aliases, @Titles, @TvSeries, @PlayedBy," +
                        "@FatherId, @MotherId, @SpouseId)", new
                        {
                            character.Id,
                            character.Name,
                            character.IsFemale,
                            character.Culture,
                            Titles = string.Join(";", character.Titles),
                            Aliases = string.Join(";", character.Aliases),
                            character.Born,
                            character.Died,
                            FatherId = character.Father,
                            MotherId = character.Mother,
                            SpouseId = character.Spouse,
                            PlayedBy = string.Join(";", character.PlayedBy),
                            TvSeries = string.Join(";", character.TvSeries)
                        });

                    foreach (var book in character.Books)
                    {
                        connection.Execute(
                            "INSERT INTO dbo.book_character_link VALUES(@BookId, @CharacterId, @Type)", new
                            {
                                BookId = book,
                                CharacterId = character.Id,
                                Type = 0
                            });
                    }
                    foreach (var povBook in character.PovBooks)
                    {
                        connection.Execute(
                            "INSERT INTO dbo.book_character_link VALUES(@BookId, @CharacterId, @Type)", new
                            {
                                BookId = povBook,
                                CharacterId = character.Id,
                                Type = 1
                            });
                    }

                    foreach (var allegiance in character.Allegiances)
                    {
                        connection.Execute("INSERT INTO dbo.character_house_link VALUES(@CharacterId, @HouseId)", new
                        {
                            CharacterId = character.Id,
                            HouseId = allegiance
                        });
                    }
                }
                Console.WriteLine("Finished inserting character data");

                foreach (var house in houses)
                {
                    var result = connection.Execute(
                        "INSERT INTO dbo.houses VALUES(@Id, @Name, @CoatOfArms, @Words, @Region, @Founded, @DiedOut, @Seats, @Titles, @AncestralWeapons," +
                        "@FounderId, @CurrentLordId, @HeirId, @OverlordId)", new
                        {
                            house.Id,
                            house.Name,
                            house.CoatOfArms,
                            house.Words,
                            house.Region,
                            house.Founded,
                            house.DiedOut,
                            Seats = string.Join(";", house.Seats),
                            Titles = string.Join(";", house.Titles),
                            AncestralWeapons = string.Join(";", house.AncestralWeapons),
                            FounderId = house.Founder,
                            CurrentLordId = house.CurrentLord,
                            HeirId = house.Heir,
                            OverlordId = house.Overlord,
                        });

                    foreach (var cadetBranch in house.CadetBranches)
                    {
                        connection.Execute("INSERT INTO dbo.house_cadetbranch_link VALUES(@HouseId, @CadetBranchHouseId)", new
                        {
                            HouseId = house.Id,
                            CadetBranchHouseId = cadetBranch
                        });
                    }
                }

                Console.WriteLine("Finished inserting house data");
            }
        }

        private static string GetScriptData(string folder, string file)
        {
            var path = folder + file;

            if (File.Exists(path))
            {
                Console.WriteLine($"Found {file}");
            }
            else
            {
                throw new ArgumentException($"Could not find {file}");
            }

            return File.ReadAllText(path);
        }

        private static List<TDto> GetDtoData<TDto>(string folder, string file)
        {
            var path = folder + file;
            if (File.Exists(path))
            {
                Console.WriteLine($"Found {file}");
            }
            else
            {
                throw new ArgumentException($"Could not find {file}");
            }

            var dtos = JsonConvert.DeserializeObject<List<TDto>>(File.ReadAllText(path));

            return dtos;
        }
    }

    public static class MediaTypeParser
    {
        public static MediaType ParseMediaType(string mediaType)
        {
            switch (mediaType)
            {
                case "Hardcover":
                    return MediaType.Hardcover;
                case "Hardback":
                    return MediaType.Hardback;
                case "Graphic Novel":
                    return MediaType.GraphicNovel;
                case "Paperback":
                    return MediaType.Paperback;
            }

            throw new ArgumentOutOfRangeException(nameof(mediaType), $"Could not parse {mediaType}");
        }
    }
}