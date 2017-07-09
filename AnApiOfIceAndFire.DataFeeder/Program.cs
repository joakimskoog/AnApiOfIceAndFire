using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Data.Houses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AnApiOfIceAndFire.DataFeeder
{
    class Program
    {

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

            var books = GetDtoData<BookDto>(folder, "books.json").Select(b => b.ToBookEntity()).ToList();
            Console.WriteLine($"Parsed {books.Count} number of books");

            var characters = GetDtoData<CharacterDto>(folder, "characters.json").Select(c => c.ToCharacterEntity()).ToList();
            Console.WriteLine($"Parsed {characters.Count} number of characters");

            var houses = GetDtoData<HouseDto>(folder, "houses.json").Select(h => h.ToHouseEntity()).ToList();
            Console.WriteLine($"Parsed {houses.Count} number of houses");

            DatabaseFeeder.CreateDatabase(initialConnectionString);
            Console.WriteLine("Created database");

            DatabaseFeeder.CreateTables(connectionString);
            Console.WriteLine("Created tables");

            var options = new OptionsWrapper<ConnectionOptions>(new ConnectionOptions()
            {
                ConnectionString = connectionString
            });

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