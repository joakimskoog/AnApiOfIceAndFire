using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Houses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ObjectCreationAsStatement

namespace AnApiOfIceAndFire.Domain.Tests.Adapters
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HouseEntityAdapterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatHouseIsNull_WhenConstructingAdapter_ThenArgumentNullExceptionIsThrown()
        {
            new HouseEntityAdapter(null);
        }

        [TestMethod]
        public void GivenThatHouseHasName_WhenAccessingAdapterName_ThenAdapterNameIsSame()
        {
            var entity = new HouseEntity { Name = "name" };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.Name, adapter.Name);
        }

        [TestMethod]
        public void GivenThatHouseHasRegion_WhenAccessingAdapterRegion_ThenAdapterRegionIsSame()
        {
            var entity = new HouseEntity { Region = "region" };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.Region, adapter.Region);
        }

        [TestMethod]
        public void GivenThatHouseHasCoatOfArms_WhenAccessingAdapterCoatOfArms_ThenAdapterCoatOfArmsIsSame()
        {
            var entity = new HouseEntity { CoatOfArms = "coat of arms" };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.CoatOfArms, adapter.CoatOfArms);
        }

        [TestMethod]
        public void GivenThatHouseHasWords_WhenAccessingAdapterWords_ThenAdapterWordsIsSame()
        {
            var entity = new HouseEntity { Words = "words" };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.Words, adapter.Words);
        }

        [TestMethod]
        public void GivenThatHouseHasOneTitle_WhenAccessingAdapterTitles_ThenAdapterTitlesContainsOne()
        {
            var entity = new HouseEntity { Titles = new[] { "titleOne" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.Titles.Length, adapter.Titles.Count);
        }

        [TestMethod]
        public void GivenThatHouseHasOneSeat_WhenAccessingAdapterSeats_ThenAdapterSeatsContainsOne()
        {
            var entity = new HouseEntity { Seats = new[] { "seat" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.Seats.Length, adapter.Seats.Count);
        }

        [TestMethod]
        public void GivenThatHouseHasFoundedData_WhenAccessingAdapterFounded_ThenAdapterFoundedIsSame()
        {
            var entity = new HouseEntity { Founded = "founded" };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.Founded, adapter.Founded);
        }

        [TestMethod]
        public void GivenThatHouseHasDiedOutData_WhenAccessingAdapterDiedOut_ThenAdapterDiedOutIsSame()
        {
            var entity = new HouseEntity { DiedOut = "Year one" };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.DiedOut, adapter.DiedOut);
        }

        [TestMethod]
        public void GivenThatHouseHasOneAncestralWeapon_WhenAccessingAdapterAncestralWeapons_ThenAdapterAncestralWeaponsAreTheSame()
        {
            var entity = new HouseEntity { AncestralWeapons = new[] { "Old Sword" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.AncestralWeapons.Length, adapter.AncestralWeapons.Count);
        }

        [TestMethod]
        public void GivenThatHouseHasNoCurrentLord_WhenAccessingAdapterCurrentLord_ThenNullIsReturned()
        {
            var entity = new HouseEntity();

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNull(adapter.CurrentLord);
        }

        [TestMethod]
        public void GivenThatHouseHasCurrentLord_WhenAccessingAdapterCurrentLord_ThenAdapterCurrentLordHasData()
        {
            var entity = new HouseEntity { CurrentLord = new CharacterEntity { Name = "currentLordName" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNotNull(adapter.CurrentLord);
            Assert.AreEqual(entity.CurrentLord.Name, adapter.CurrentLord.Name);
        }

        [TestMethod]
        public void GivenThatHouseHasNoHeir_WhenAccessingAdapterHeir_ThenNullIsReturned()
        {
            var entity = new HouseEntity();

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNull(adapter.Heir);
        }

        [TestMethod]
        public void GivenThatHouseHasHeir_WhenAccessingAdapterHeir_ThenAdapterHeirHasData()
        {
            var entity = new HouseEntity { Heir = new CharacterEntity { Name = "heirName" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNotNull(adapter.Heir);
            Assert.AreEqual(entity.Heir.Name, adapter.Heir.Name);
        }

        [TestMethod]
        public void GivenThatHouseHasNoOverlord_WhenAccessingAdapterOverlord_ThenNullIsReturned()
        {
            var entity = new HouseEntity();

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNull(adapter.Overlord);
        }

        [TestMethod]
        public void GivenThatHouseHasOverlord_WhenAccessingAdapterOverlord_ThenAdapterOverlordHasData()
        {
            var entity = new HouseEntity { Overlord = new HouseEntity { Name = "overlordName" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNotNull(adapter.Overlord);
            Assert.AreEqual(entity.Overlord.Name, adapter.Overlord.Name);
        }

        [TestMethod]
        public void GivenThatHouseHasNoFounder_WhenAccessingAdapterFounder_ThenNullIsReturned()
        {
            var entity = new HouseEntity();

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNull(adapter.Founder);
        }

        [TestMethod]
        public void GivenThatHouseHasFounder_WhenAccessingAdapterFounder_ThenAdapterFounderHasData()
        {
            var entity = new HouseEntity { Founder = new CharacterEntity() { Name = "founderName" } };

            var adapter = new HouseEntityAdapter(entity);

            Assert.IsNotNull(adapter.Founder);
            Assert.AreEqual(entity.Founder.Name, adapter.Founder.Name);
        }

        [TestMethod]
        public void GivenThatHouseHasOneCadetBranch_WhenAccessingAdapterCadetBranches_ThenAdapterHasOneCadetBranch()
        {
            var entity = new HouseEntity {CadetBranches = new List<HouseEntity> {new HouseEntity()} };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.CadetBranches.Count, adapter.CadetBranches.Count);
        }

        [TestMethod]
        public void GivenThatHouseHasOneSwornMember_WhenAccessingAdapterSwornMembers_ThenAdapterHasOneSwornMember()
        {
            var entity = new HouseEntity {SwornMembers = new List<CharacterEntity> {new CharacterEntity()} };

            var adapter = new HouseEntityAdapter(entity);

            Assert.AreEqual(entity.SwornMembers.Count, adapter.SwornMembers.Count);
        }
    }
}