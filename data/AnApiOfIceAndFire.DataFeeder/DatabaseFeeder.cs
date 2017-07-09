using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Data.Houses;
using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;

namespace AnApiOfIceAndFire.DataFeeder
{
    public static class DatabaseFeeder
    {
        public static void CreateDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.CreateDatabase);
            }
        }

        public static void CreateTables(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.CreateBooksTable);
                connection.Execute(DatabaseScripts.CreateCharactersTable);
                connection.Execute(DatabaseScripts.CreateHousesTable);
                connection.Execute(DatabaseScripts.CreateBookCharacterLinkTable);
                connection.Execute(DatabaseScripts.CreateCharacterHouseLinkTable);
                connection.Execute(DatabaseScripts.CreateHouseCadetBranchLinkTable);
            }
        }

        public static void DeleteDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.DeleteDatabase);
            }
        }

        public static void CleanDatabase(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(DatabaseScripts.TruncateAllTables);
            }
        }

        public static void RunAllScripts(string masterConnectionString, string dbConnectionString)
        {
            var books = GetDtoData<BookDto>(@"..\..\..\..\..\data\books.json").Select(b => b.ToBookEntity()).ToList();
            Console.WriteLine($"Parsed {books.Count} number of books");

            var characters = GetDtoData<CharacterDto>(@"..\..\..\..\..\data\characters.json").Select(c => c.ToCharacterEntity()).ToList();
            Console.WriteLine($"Parsed {characters.Count} number of characters");

            var houses = GetDtoData<HouseDto>(@"..\..\..\..\..\data\houses.json").Select(h => h.ToHouseEntity()).ToList();
            Console.WriteLine($"Parsed {houses.Count} number of houses");

#if DEBUG
            try
            {
                DatabaseFeeder.CleanDatabase(masterConnectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
#endif
            try
            {
                DatabaseFeeder.CreateDatabase(masterConnectionString);
                Console.WriteLine("Created database");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create database: " + e.Message);
            }

            try
            {
                DatabaseFeeder.CreateTables(dbConnectionString);
                Console.WriteLine("Created tables");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create tables: " + e.Message);
            }


            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = dbConnectionString
            });

            try
            {
                var bookRepo = new BookRepository(options);
                bookRepo.InsertEntitiesAsync(books).GetAwaiter().GetResult();
                Console.WriteLine("Finished inserting book data");

                var characterRepo = new CharacterRepository(options);
                characterRepo.InsertEntitiesAsync(characters).GetAwaiter().GetResult();
                Console.WriteLine("Finished inserting character data");

                var houseRepo = new HouseRepository(options);
                houseRepo.InsertEntitiesAsync(houses).GetAwaiter().GetResult();
                Console.WriteLine("Finished inserting house data");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static List<TDto> GetDtoData<TDto>(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find {Path.GetFullPath(path)}");
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