using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using EntityFramework.Utilities;
using Newtonsoft.Json;

namespace AnApiOfIceAndFire.Data.Feeder
{
    public static class Feeder
    {
        public static void FeedDatabase(AnApiOfIceAndFireContext context, string booksUrl, string charactersUrl, string housesUrl, Action<string> printAction)
        {
            var sw = new Stopwatch();

            sw.Start();
            var bookDtos = JsonConvert.DeserializeObject<List<BookDto>>(File.ReadAllText(booksUrl));
            sw.Stop();
            printAction($"Deserialization of books: {sw.ElapsedMilliseconds}");

            sw.Restart();
            var characterDtos = JsonConvert.DeserializeObject<List<CharacterDto>>(File.ReadAllText(charactersUrl));

            sw.Stop();
            printAction($"Deserialization of characters: {sw.ElapsedMilliseconds}");

            sw.Restart();
            var houseDtos = JsonConvert.DeserializeObject<List<HouseDto>>(File.ReadAllText(housesUrl));
            sw.Stop();
            printAction($"Deserialization of houses: {sw.ElapsedMilliseconds}");


            IDictionary<int, CharacterEntity> charactersInDatabase =
                context.Characters.Include("Books")
                    .Include("PovBooks")
                    .Include("Allegiances")
                    .ToDictionary(entity => entity.Id);
            var booksInDatabase = context.Books.ToDictionary(book => book.Id);
            var housesInDatabase = context.Houses.Include("CadetBranches").ToDictionary(house => house.Id);

            sw.Restart();
            var books = bookDtos.Select(b => new BookEntity()
            {
                Id = b.Id,
                Name = b.Name,
                Authors = b.Authors,
                Country = b.Country,
                ISBN = b.ISBN,
                NumberOfPages = b.NumberOfPages,
                Publisher = b.Publisher,
                ReleaseDate = b.ReleaseDate
            }).ToList();

            var characters = characterDtos.Select(c => new CharacterEntity()
            {
                Id = c.Id,
                Name = c.Name,
                IsFemale = c.IsFemale,
                Born = c.Born,
                Culture = c.Culture,
                Died = c.Died,
                Aliases = c.Aliases,
                PlayedBy = c.PlayedBy,
                Titles = c.Titles,
                TvSeries = c.TvSeries,
                FatherId = c.Father,
                MotherId = c.Mother,
                SpouseId = c.Spouse
            }).ToList();

            var houses = houseDtos.Select(h => new HouseEntity()
            {
                Id = h.Id,
                Name = h.Name,
                CoatOfArms = h.CoatOfArms,
                Founded = h.Founded,
                DiedOut = h.DiedOut,
                Region = h.Region,
                Words = h.Words,
                Titles = h.Titles,
                AncestralWeapons = h.AncestralWeapons,
                Seats = h.Seats,
                CurrentLordId = h.CurrentLord,
                HeirId = h.Heir,
                OverlordId = h.Overlord,
                FounderId = h.Founder
            }).ToList();
            sw.Stop();
            printAction($"Mapping: {sw.ElapsedMilliseconds}");

            sw.Restart();
            var booksToUpdate = books.Where(x => booksInDatabase.ContainsKey(x.Id));
            var booksToInsert = books.Where(x => !booksInDatabase.ContainsKey(x.Id));

            EFBatchOperation.For(context, context.Books).InsertAll(booksToInsert);
            EFBatchOperation.For(context, context.Books).UpdateAll(booksToUpdate, x => x.ColumnsToUpdate(y => y.Name, y => y.AuthorsRaw,
                y => y.Country, y => y.ISBN, y => y.NumberOfPages, y => y.Publisher, y => y.ReleaseDate));

            sw.Stop();
            printAction($"Adding books: {sw.ElapsedMilliseconds}");

            sw.Restart();
            var charactersToUpdate = characters.Where(x => charactersInDatabase.ContainsKey(x.Id));
            var charactersToInsert = characters.Where(x => !charactersInDatabase.ContainsKey(x.Id));
            EFBatchOperation.For(context, context.Characters).InsertAll(charactersToInsert);
            EFBatchOperation.For(context, context.Characters).UpdateAll(charactersToUpdate, x => x.ColumnsToUpdate(y => y.Name, y => y.Born, y => y.Culture,
                y => y.Died, y => y.AliasesRaw, y => y.PlayedByRaw, y => y.TitlesRaw, y => y.TvSeriesRaw, y => y.FatherId, y => y.MotherId, y => y.SpouseId,
                y => y.IsFemale));
            sw.Stop();
            printAction($"Adding characters: {sw.ElapsedMilliseconds}");

            sw.Restart();
            var housesToUpdate = houses.Where(x => housesInDatabase.ContainsKey(x.Id));
            var housesToInsert = houses.Where(x => !housesInDatabase.ContainsKey(x.Id));
            EFBatchOperation.For(context, context.Houses).InsertAll(housesToInsert);
            EFBatchOperation.For(context, context.Houses).UpdateAll(housesToUpdate, x => x.ColumnsToUpdate(y => y.Name, y => y.CoatOfArms,
                y => y.Founded, y => y.DiedOut, y => y.Region, y => y.Words, y => y.TitlesRaw, y => y.AncestralWeaponsRaw, y => y.SeatsRaw,
                y => y.FounderId, y => y.CurrentLordId, y => y.HeirId, y => y.OverlordId));

            sw.Stop();
            printAction($"Adding houses: {sw.ElapsedMilliseconds}");

            sw.Restart();
            context = new AnApiOfIceAndFireContext();
            charactersInDatabase =
                context.Characters.Include("Books")
                    .Include("PovBooks")
                    .Include("Allegiances")
                    .ToDictionary(entity => entity.Id);
            booksInDatabase = context.Books.ToDictionary(book => book.Id);
            housesInDatabase = context.Houses.Include("CadetBranches").ToDictionary(house => house.Id);

            foreach (var character in characterDtos)
            {
                var characterToUpdate = charactersInDatabase[character.Id];

                foreach (var book in character.Books)
                {
                    var bookToLookup = booksInDatabase[book];
                    if (bookToLookup != null)
                    {
                        if (characterToUpdate.Books.All(x => x.Id != bookToLookup.Id))
                        {
                            characterToUpdate.Books.Add(bookToLookup);
                        }
                    }
                }
                foreach (var povBook in character.PovBooks)
                {
                    var bookToLookup = booksInDatabase[povBook];
                    if (bookToLookup != null)
                    {
                        if (characterToUpdate.PovBooks.All(x => x.Id != bookToLookup.Id))
                        {
                            characterToUpdate.PovBooks.Add(bookToLookup);
                        }
                    }
                }
                foreach (var allegiance in character.Allegiances)
                {
                    var houseToLookup = housesInDatabase[allegiance];
                    if (houseToLookup != null)
                    {
                        if (characterToUpdate.Allegiances.All(x => x.Id != houseToLookup.Id))
                        {
                            characterToUpdate.Allegiances.Add(houseToLookup);
                        }
                    }
                }
            }

            context.ChangeTracker.DetectChanges();
            context.SaveChanges();
            sw.Stop();
            printAction($"Characters: {sw.ElapsedMilliseconds}");

            sw.Restart();
            context = new AnApiOfIceAndFireContext();
            foreach (var house in houseDtos)
            {
                var houseToUpdate = housesInDatabase[house.Id];

                foreach (var cadetBranch in house.CadetBranches)
                {
                    var cadetBranchHouse = housesInDatabase[cadetBranch];
                    if (houseToUpdate.CadetBranches.All(x => x.Id != cadetBranchHouse.Id))
                    {
                        houseToUpdate.CadetBranches.Add(cadetBranchHouse);
                    }
                }
            }

            context.ChangeTracker.DetectChanges();
            context.SaveChanges();
            sw.Stop();
            context.ChangeTracker.DetectChanges();
            printAction($"Houses: {sw.ElapsedMilliseconds}");
        }
    }
}