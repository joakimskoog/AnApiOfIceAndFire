using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ObjectCreationAsStatement

namespace AnApiOfIceAndFire.Domain.Tests.Adapters
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CharacterEntityAdapterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatCharacterIsNull_WhenConstructingAdapter_ThenArgumentNullExceptionIsThrown()
        {
            new CharacterEntityAdapter(null);
        }

        [TestMethod]
        public void GivenThatCharacterHasName_WhenAccessingAdapterName_ThenAdapterNameIsSameAsCharacterName()
        {
            var entity = new CharacterEntity { Name = "name" };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Name, adapter.Name);
        }

        [TestMethod]
        public void GivenThatCharacterHasCulture_WhenAccessingAdapterCulture_ThenAdapterCultureIsSameAsCharacterCulture()
        {
            var entity = new CharacterEntity { Culture = "culture" };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Culture, adapter.Culture);
        }

        [TestMethod]
        public void GivenThatCharacterHasBornData_WhenAccessingAdapterBorn_ThenAdapterBornIsSameAsCharacterBorn()
        {
            var entity = new CharacterEntity { Born = "Year 1" };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Born, adapter.Born);
        }

        [TestMethod]
        public void GivenThatCharacterHasDiedData_WhenAccessingAdapterDied_ThenAdapterDiedIsSameAsCharacterDied()
        {
            var entity = new CharacterEntity { Died = "Year 2" };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Died, adapter.Died);
        }

        [TestMethod]
        public void GivenThatCharacterHasOneTitle_WhenAccessingAdapterTitles_ThenAdapterTitlesContainsOneTitle()
        {
            var entity = new CharacterEntity { Titles = new[] { "titleOne" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Titles.Length, adapter.Titles.Count);
        }

        [TestMethod]
        public void GivenThatCharacterHasOneAlias_WhenAccessingAdapterAliases_ThenAdapterAliasesContainsOneAlias()
        {
            var entity = new CharacterEntity { Aliases = new[] { "aliasOne" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Aliases.Length, adapter.Aliases.Count);
        }

        [TestMethod]
        public void GivenThatCharacterHasOneTvSeries_WhenAccessingAdapterTvSeries_ThenAdapterContainsOneTvSeries()
        {
            var entity = new CharacterEntity { TvSeries = new[] { "Season 1" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.TvSeries.Length, adapter.TvSeries.Count);
        }

        [TestMethod]
        public void GivenThatCharacterHasOnePlayedBy_WhenAccessingAdapterPlayedBy_ThenAdapterContainsOnePlayedBy()
        {
            var entity = new CharacterEntity { PlayedBy = new[] { "Some Actor" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.PlayedBy.Length, adapter.PlayedBy.Count);
        }

        [TestMethod]
        public void GivenThatCharacterDoesNotHaveFather_WhenAccessingAdapterFather_ThenAdapterFatherReturnsNull()
        {
            var entity = new CharacterEntity();

            var adapter = new CharacterEntityAdapter(entity);

            Assert.IsNull(adapter.Father);
        }

        [TestMethod]
        public void GivenThatCharacterHasFather_WhenAccessingAdapterFather_ThenAdapterFatherIsTheCorrectOne()
        {
            var entity = new CharacterEntity { Name = "name", Father = new CharacterEntity() { Name = "fatherName" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.IsNotNull(adapter.Father);
            Assert.AreEqual(entity.Father.Name, adapter.Father.Name);
        }

        [TestMethod]
        public void GivenThatCharacterDoesNotHaveMother_WhenAccessingAdapterMother_ThenAdapterMotherReturnsNull()
        {
            var entity = new CharacterEntity();

            var adapter = new CharacterEntityAdapter(entity);

            Assert.IsNull(adapter.Mother);
        }

        [TestMethod]
        public void GivenThatCharacterHasMother_WhenAccessingAdapterMother_ThenAdapterMotherIsTheCorrectOne()
        {
            var entity = new CharacterEntity { Name = "name", Mother = new CharacterEntity() { Name = "motherName" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.IsNotNull(adapter.Mother);
            Assert.AreEqual(entity.Mother.Name, adapter.Mother.Name);
        }

        [TestMethod]
        public void GivenThatCharacterDoesNotHaveSpouse_WhenAccessingAdapterSpouse_ThenAdapterSpouseReturnsNull()
        {
            var entity = new CharacterEntity();

            var adapter = new CharacterEntityAdapter(entity);

            Assert.IsNull(adapter.Spouse);
        }

        [TestMethod]
        public void GivenThatCharacterHasSpouse_WhenAccessingAdapterSpouse_ThenAdapterSpouseIsTheCorrectOne()
        {
            var entity = new CharacterEntity { Name = "name", Spouse = new CharacterEntity() { Name = "spouseNae" } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.IsNotNull(adapter.Spouse);
            Assert.AreEqual(entity.Spouse.Name, adapter.Spouse.Name);
        }

        [TestMethod]
        public void GivenThatCharacterHasOneAllegiance_WhenAccessingAdapterAllegiances_ThenAdapterAllegiancesContainsOne()
        {
            var entity = new CharacterEntity { Allegiances = new List<HouseEntity> { new HouseEntity { Name = "houseName" } } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Allegiances.Count, adapter.Allegiances.Count);
        }

        [TestMethod]
        public void GivenThatCharacterHasOneBook_WhenAccessingAdapterBooks_ThenAdapterBooksContainsOne()
        {
            var entity = new CharacterEntity { Books = new List<BookEntity> { new BookEntity() } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.Books.Count, adapter.Books.Count);
        }

        [TestMethod]
        public void GivenThatCharacterHasOnePovBook_WhenAccessingAdapterPovBooks_ThenAdapterPovBooksContainsOne()
        {
            var entity = new CharacterEntity { PovBooks = new List<BookEntity> { new BookEntity() } };

            var adapter = new CharacterEntityAdapter(entity);

            Assert.AreEqual(entity.PovBooks.Count, adapter.PovBooks.Count);
        }
    }
}