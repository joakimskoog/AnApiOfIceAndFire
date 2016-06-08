using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Characters;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using AnApiOfIceAndFire.Domain.Houses;

namespace AnApiOfIceAndFire.Tests.Models.v0.Mappers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HouseMapperTests
    {
        private const string RequestedUri = "http://localhost/api/houses/1";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatHouseIsNull_WhenTryingToMapIt_ThenArgumentNullExceptionIsThrown()
        {
            var urlHelper = CreateUrlHelper(RequestedUri);
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(null, urlHelper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatUrlHelperIsNull_WhenTryingToMapCharacter_ThenArgumentNullExceptionIsThrown()
        {
            var house = MockRepository.GenerateMock<IHouse>();
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, null);
        }

        [TestMethod]
        public void GivenThatHouseHasIdentifier_WhenTryingToMapIt_ThenCharacterUrlIsCorrectAndContainsIdentifier()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(RequestedUri, mappedHouse.URL);
        }

        [TestMethod]
        public void GivenThatHouseHasCurrentLord_WhenTryingToMapIt_ThenMappedHouseCurrentLordHasCorrectUrl()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var currentLord = MockRepository.GenerateMock<ICharacter>();
            currentLord.Stub(x => x.Identifier).Return(12);
            house.Stub(x => x.CurrentLord).Return(currentLord);
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual($"http://localhost/api/characters/{house.CurrentLord.Identifier}", mappedHouse.CurrentLord);
        }

        [TestMethod]
        public void GivenThatHouseHasHeir_WhenTryingToMapIt_ThenMappedHouseHeirHasCorrectUrl()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var heir = MockRepository.GenerateMock<ICharacter>();
            heir.Stub(x => x.Identifier).Return(37);
            house.Stub(x => x.Heir).Return(heir);
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual($"http://localhost/api/characters/{house.Heir.Identifier}", mappedHouse.Heir);
        }

        [TestMethod]
        public void GivenThatHouseHasOverlord_WhenTryingToMapIt_ThenMappedHouseOverlordHasCorrectUrl()
        {
            var house = CreateMockedHouse(13, "testHouse");
            var overlord = CreateMockedHouse(37, "overlordHouse");
            house.Stub(x => x.Overlord).Return(overlord);
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual($"http://localhost/api/houses/{house.Overlord.Identifier}", mappedHouse.Overlord);
        }

        [TestMethod]
        public void GivenThatHouseHasFounder_WhenTryingToMapIt_ThenMappedHouseFounderHasCorrectUrl()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var founder = MockRepository.GenerateMock<ICharacter>();
            founder.Stub(x => x.Identifier).Return(37);
            house.Stub(x => x.Founder).Return(founder);
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual($"http://localhost/api/characters/{house.Founder.Identifier}", mappedHouse.Founder);
        }

        [TestMethod]
        public void GivenThatHouseHasCadetBranches_WhenTryingToMapIt_ThenMappedHouseCadetBranchesContainsCorrectUrls()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var cadetBranch = CreateMockedHouse(2, "cadetBranch");
            house.Stub(x => x.CadetBranches).Return(new List<IHouse> { cadetBranch });
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(1, mappedHouse.CadetBranches.Count());
            Assert.AreEqual($"http://localhost/api/houses/{cadetBranch.Identifier}", mappedHouse.CadetBranches.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatHouseHasSwornMembers_WhenTryingToMapIt_ThenMappedHouseSwornMembersContainsCorrectUrls()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var swornMember = MockRepository.GenerateMock<ICharacter>();
            swornMember.Stub(x => x.Identifier).Return(1000);
            house.Stub(x => x.SwornMembers).Return(new List<ICharacter> { swornMember });
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(1, mappedHouse.SwornMembers.Count());
            Assert.AreEqual($"http://localhost/api/characters/{swornMember.Identifier}", mappedHouse.SwornMembers.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatHouseHasName_WhenTryingToMapIt_ThenMappedHouseHasSameName()
        {
            var house = CreateMockedHouse(1, "testHouse");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.Name, mappedHouse.Name);
        }

        [TestMethod]
        public void GivenThatHouseHasRegion_WhenTryingToMapIt_ThenMappedHouseHasSameRegion()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.Region).Return("region");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.Region, mappedHouse.Region);
        }

        [TestMethod]
        public void GivenThatHouseHasCoatOfArms_WhenTryingToMapIt_ThenMappedHouseHasSameCoatOfArms()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.CoatOfArms).Return("Coat of arms");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.CoatOfArms, mappedHouse.CoatOfArms);
        }

        [TestMethod]
        public void GivenThatHouseHasWords_WhenTryingToMapIt_ThenMappedHouseHasSameWords()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.Words).Return("Winter is Coming");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.Words, mappedHouse.Words);
        }

        [TestMethod]
        public void GivenThatHouseWasFounded_WhenTryingToMapIt_ThenMappedHouseHasSameFoundedData()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.Founded).Return("Some year");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.Founded, mappedHouse.Founded);
        }

        [TestMethod]
        public void GivenThatHouseHasDiedOut_WhenTryingToMapIt_ThenMappedHouseHasSameDiedOutData()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.DiedOut).Return("Some year");
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.DiedOut, mappedHouse.DiedOut);
        }

        [TestMethod]
        public void GivenThatHouseHasTitles_WhenTryingToMapIt_ThenMappedHouseHasSameTitles()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.Titles).Return(new List<string> { "titleOne, titleTwo" });
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.Titles.Count, mappedHouse.Titles.Count());
            Assert.AreEqual(house.Titles.ElementAt(0), mappedHouse.Titles.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatHouseHasSeats_WhenTryingToMapIt_ThenMappedHouseHasSameSeats()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.Seats).Return(new List<string> { "seatOne" });
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.Seats.Count, mappedHouse.Seats.Count());
            Assert.AreEqual(house.Seats.ElementAt(0), mappedHouse.Seats.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatHouseHasAncestralWeapons_WhenTryingToMapIt_ThenMappedHouseHasSameAncestralWeapons()
        {
            var house = CreateMockedHouse(1, "testHouse");
            house.Stub(x => x.AncestralWeapons).Return(new List<string> { "Cool Old Weapon" });
            var mapper = new HouseMapper();

            var mappedHouse = mapper.Map(house, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(house.AncestralWeapons.Count, mappedHouse.AncestralWeapons.Count());
            Assert.AreEqual(house.AncestralWeapons.ElementAt(0), mappedHouse.AncestralWeapons.ElementAt(0));
        }

        private static IHouse CreateMockedHouse(int id, string name)
        {
            var house = MockRepository.GenerateMock<IHouse>();
            house.Stub(x => x.Identifier).Return(id);
            house.Stub(x => x.Name).Return(name);

            return house;
        }

        private static UrlHelper CreateUrlHelper(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: BookLinkCreator.SingleBookRouteName,
                routeTemplate: "api/books/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            configuration.Routes.MapHttpRoute(
               name: CharacterLinkCreator.SingleCharacterRouteName,
               routeTemplate: "api/characters/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            configuration.Routes.MapHttpRoute(
               name: HouseLinkCreator.SingleHouseRouteName,
               routeTemplate: "api/houses/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);

            var urlHelper = new UrlHelper(requestMessage);

            return urlHelper;
        }
    }
}