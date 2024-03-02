using Dapper;
using Microsoft.Data.Sqlite;

namespace AnApiOfIceAndFire.Database.Seeder
{
    public static class SetupHelper
    {
        public static string SetupDatabase(string databasePath = null)
        {
            var dbPath = databasePath ?? FindDatabasePath();
            DeleteExistingDatabaseFiles(dbPath);

            var (books, characters, houses) = Parser.Parse();

            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();
            using var tran = connection.BeginTransaction();

            var setupSql = File.ReadAllText("setup.sql");
            connection.Execute(setupSql);

            try
            {
                for (int i = 1; i < books.Length; i++)
                {
                    var book = books[i];
                    connection.Execute(@"INSERT INTO Books (Id, Name, ISBN, Authors, NumberOfPages, Publisher, MediaType, Country, ReleaseDate) 
                         VALUES(@Id, @Name, @ISBN, @Authors, @NumberOfPages, @Publisher, @MediaType, @Country, @ReleaseDate)",
                                         new
                                         {
                                             book.Id,
                                             book.Name,
                                             book.ISBN,
                                             book.Authors,
                                             book.NumberOfPages,
                                             book.Publisher,
                                             book.MediaType,
                                             book.Country,
                                             book.ReleaseDate
                                         });
                }

                for (int i = 1; i < characters.Length; i++)
                {
                    var character = characters[i];
                    connection.Execute(@"INSERT INTO Characters (Id, Name, Culture, Born, Died, Gender, Aliases, Titles, TvSeries, PlayedBy, FatherId, MotherId, SpouseId)
                         VALUES(@Id, @Name, @Culture, @Born, @Died, @Gender, @Aliases, @Titles, @TvSeries, @PlayedBy, @FatherId, @MotherId, @SpouseId)",
                                         new
                                         {
                                             character.Id,
                                             character.Name,
                                             character.Culture,
                                             character.Born,
                                             character.Died,
                                             character.Gender,
                                             character.Aliases,
                                             character.Titles,
                                             character.TvSeries,
                                             character.PlayedBy,
                                             character.FatherId,
                                             character.MotherId,
                                             character.SpouseId
                                         });

                    foreach (var bookId in character.BookIdentifiers)
                    {
                        connection.Execute("INSERT INTO BookCharacters (BookId, CharacterId, Type) VALUES(@BookId, @CharacterId, @Type)",
                            new
                            {
                                @BookId = bookId,
                                @CharacterId = character.Id,
                                @Type = 0
                            });
                    }

                    foreach (var povBookId in character.PovBookIdentifiers)
                    {
                        connection.Execute("INSERT INTO BookCharacters (BookId, CharacterId, Type) VALUES(@BookId, @CharacterId, @Type)",
                            new
                            {
                                @BookId = povBookId,
                                @CharacterId = character.Id,
                                @Type = 1
                            });
                    }
                }

                for (int i = 1; i < houses.Length; i++)
                {
                    var house = houses[i];
                    connection.Execute(@"INSERT INTO Houses (Id, Name, CoatOfArms, Words, Region, Founded, DiedOut, Seats, Titles, AncestralWeapons, FounderId, CurrentLordId, HeirId, MainBranchId, OverlordId)
                         VALUES(@Id, @Name, @CoatOfArms, @Words, @Region, @Founded, @DiedOut, @Seats, @Titles, @AncestralWeapons, @FounderId, @CurrentLordId, @HeirId, @MainBranchId, @OverlordId)",
                                         new
                                         {
                                             house.Id,
                                             house.Name,
                                             house.CoatOfArms,
                                             house.Words,
                                             house.Region,
                                             house.Founded,
                                             house.DiedOut,
                                             house.Seats,
                                             house.Titles,
                                             house.AncestralWeapons,
                                             house.FounderId,
                                             house.CurrentLordId,
                                             house.HeirId,
                                             house.MainBranchId,
                                             house.OverlordId
                                         });

                    foreach (var characterId in house.SwornMemberIdentifiers)
                    {
                        connection.Execute("INSERT INTO HouseCharacters (HouseId, CharacterId) VALUES(@HouseId, @CharacterId)",
                            new
                            {
                                @HouseId = house.Id,
                                @CharacterId = characterId
                            });
                    }
                }

                tran.Commit();

                return dbPath;
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }

        private static string FindDatabasePath()
        {
            var rootDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (rootDirectory != null && rootDirectory.GetFiles("AnApiOfIceAndFire.sln").Length == 0)
            {
                rootDirectory = rootDirectory.Parent;
            }

            return Path.Join(rootDirectory.FullName, "src", "AnApiOfIceAndFire", "Resources", "anapioficeandfire.db");
        }

        private static void DeleteExistingDatabaseFiles(string databasePath)
        {
            var folderPath = Path.GetDirectoryName(databasePath);
            foreach (var file in Directory.GetFiles(folderPath).Where(fp => Path.GetFileName(fp).StartsWith("anapioficeandfire.db")))
            {
                File.Delete(file);
            }
        }
    }
}