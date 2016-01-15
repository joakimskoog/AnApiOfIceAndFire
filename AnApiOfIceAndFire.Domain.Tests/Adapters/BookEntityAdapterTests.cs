using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ObjectCreationAsStatement

namespace AnApiOfIceAndFire.Domain.Tests.Adapters
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BookEntityAdapterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatBookEntityIsNull_WhenConstructingAdapter_ThenArgumentNullExceptionsIsThrown()
        {
            new BookEntityAdapter(null);
        }

        [TestMethod]
        public void GivenThatBookHasName_WhenAccessingAdapterName_ThenAdapterNameIsSameAsBook()
        {
            var entity = new BookEntity { Name = "bookName" };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.Name, adapter.Name);
        }

        [TestMethod]
        public void GivenThatBookHasIsbn_WhenAccessingAdapterIsbn_ThenAdapterIsbnIsSameAsBook()
        {
            var entity = new BookEntity { ISBN = "isbn" };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.ISBN, adapter.ISBN);
        }

        [TestMethod]
        public void GivenThatBookHasPublisher_WhenAccessingAdapterPublisher_ThenAdapterPublisherIsSameAsBook()
        {
            var entity = new BookEntity { Publisher = "publisher" };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.Publisher, adapter.Publisher);
        }

        [TestMethod]
        public void GivenThatBookHasCountry_WhenAccessingAdapterCountry_ThenAdapterCountryIsSameAsBook()
        {
            var entity = new BookEntity { Country = "Sweden" };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.Country, adapter.Country);
        }

        [TestMethod]
        public void GivenThatBookHasReleaseDate_WhenAccessingAdapterReleaseDate_ThenAdapterReleaseDateIsSameAsBook()
        {
            var entity = new BookEntity { ReleaseDate = new DateTime(2000, 1, 1) };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.ReleaseDate, adapter.Released);
        }

        [TestMethod]
        public void GivenThatBookHasNumberOfPages_WhenAccessingAdapterNumberOfPages_ThenAdapterNumberOfPagesIsSameAsBook()
        {
            var entity = new BookEntity { NumberOfPages = 312 };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.NumberOfPages, adapter.NumberOfPages);
        }

        [TestMethod]
        public void GivenThatBookHasOneAuthor_WhenAccessingAdapterAuthors_ThenAdapterHasOneAuthor()
        {
            var entity = new BookEntity { Authors = new[] { "Joakim Skoog" } };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.Authors.Count(), adapter.Authors.Count);
        }

        [TestMethod]
        public void GivenThatBookHasOneCharacter_WhenAccessingCharacters_ThenAdapterCharactersContaineOneCharacter()
        {
            var entity = new BookEntity { Characters = new List<CharacterEntity> { new CharacterEntity { Id = 1, Name = "characterName" } } };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.Characters.Count, adapter.Characters.Count);
        }

        [TestMethod]
        public void GivenThatBookHasOnePovCharacter_WhenAccessingPovCharacters_ThenAdapterPovCharactersContaineOneCharacter()
        {
            var entity = new BookEntity { PovCharacters = new List<CharacterEntity> { new CharacterEntity { Id = 1, Name = "characterName" } } };

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(entity.PovCharacters.Count, adapter.POVCharacters.Count);
        }

        [TestMethod]
        public void GivenThatBookHasHardcoverMediaType_WhenAccessingAdapterMediaType_ThenAdapterMediaTypeIsHardcover()
        {
            var entity = new BookEntity {MediaType = MediaTypeEntity.Hardcover};

            var adapter = new BookEntityAdapter(entity);

            Assert.AreEqual(MediaType.Hardcover, adapter.MediaType);
        }
    }
}