using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;
using Geymsla.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace AnApiOfIceAndFire.Tests.IntegrationTests
{
    [TestClass]
    public class HousesControllerTests : DbIntegrationTestHarness
    {
        [TestMethod]
        public async Task GivenThatNoHouseExists_WhenTryingToGetHouseWithidOne_ThenNoHouseIsReturnedAndResponseIsNotFound()
        {
            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses/1");
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Get(id: 1);
            var houseResult = response as NotFoundResult;

            Assert.IsNotNull(houseResult);
        }

        [TestMethod]
        public async Task GivenThatHouseWithIdOneExists_WhenTryingToGetHouseWithIdOne_ThenHouseWithIdOneIsReturned()
        {
            SeedDatabase(new HouseEntity
            {
                Id = 1,
                Name = "houseOne",
                AncestralWeapons = new[] { "coolWeapon" },
                Seats = new[] { "seatOne" },
                Titles = new[] { "titleOne" }
            });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses/1");
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Get(id: 1);
            var houseResult = (response as OkNegotiatedContentResult<House>).Content;

            Assert.IsNotNull(houseResult);
            Assert.AreEqual("houseOne", houseResult.Name);
            Assert.AreEqual("http://localhost.com/api/houses/1", houseResult.URL);
        }

        [TestMethod]
        public async Task GivenThatThreeHousesExistsAndNoFilterParameterInRequest_WhenTryingToGetHouses_ThenAllHousesAreReturned()
        {
            SeedDatabase(new HouseEntity
            {
                Id = 1,
                Name = "houseOne",
                AncestralWeapons = new[] { "weapon" },
                Seats = new[] { "seatOne" },
                Titles = new[] { "titleOne" }
            },
            new HouseEntity
            {
                Id = 2,
                Name = "houseTwo",
                AncestralWeapons = new[] { "weapon" },
                Seats = new[] { "seatOne" },
                Titles = new[] { "titleOne" }
            },
            new HouseEntity
            {
                Id = 3,
                Name = "houseThree",
                AncestralWeapons = new[] { "weapon" },
                Seats = new[] { "seatOne" },
                Titles = new[] { "titleOne" }
            });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get();
            result.TryGetContentValue(out houses);

            Assert.IsNotNull(houses);
            Assert.AreEqual(3, houses.Count());
        }

        [TestMethod]
        public async Task GivenTwoHousesNameFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" }
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" }
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(name: "houseTwo");
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("houseTwo", house.Name);
        }

        [TestMethod]
        public async Task GivenTwoHousesRegionFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The North"
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(region: "The Crownlands");
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("The Crownlands", house.Region);
        }

        [TestMethod]
        public async Task GivenTwoHousesWordsFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The North",
                    Words = "Cool Words, Yo"
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands",
                    Words = "Cooler Words"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(words: "Cooler Words");
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("Cooler Words", house.Words);
        }

        [TestMethod]
        public async Task GivenTwoHousesHasWordsFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The North",
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands",
                    Words = "Cooler Words"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(hasWords: true);
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("Cooler Words", house.Words);
        }

        [TestMethod]
        public async Task GivenTwoHousesHasTitlesFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Region = "The North",
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands",
                    Words = "Cooler Words"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(hasTitles: true);
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("houseTwo", house.Name);
        }

        [TestMethod]
        public async Task GivenTwoHousesHasSeatsFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Region = "The North",
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands",
                    Words = "Cooler Words"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(hasSeats: true);
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("houseTwo", house.Name);
        }

        [TestMethod]
        public async Task GivenTwoHousesHasDiedOutFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    AncestralWeapons = new[] { "weapon" },
                    Region = "The North",
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands",
                    Words = "Cooler Words",
                    DiedOut = "200 AC"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(hasDiedOut: true);
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("houseTwo", house.Name);
        }

        [TestMethod]
        public async Task GivenTwoHousesHasAncestralWeaponsFilterParameterMatchingOneHouse_WhenTryingToGetHouses_ThenOneHouseIsReturned()
        {
            SeedDatabase(
                new HouseEntity
                {
                    Id = 1,
                    Name = "houseOne",
                    Region = "The North",
                },
                new HouseEntity
                {
                    Id = 2,
                    Name = "houseTwo",
                    AncestralWeapons = new[] { "weapon" },
                    Seats = new[] { "seatOne" },
                    Titles = new[] { "titleOne" },
                    Region = "The Crownlands",
                    Words = "Cooler Words",
                    DiedOut = "200 AC"
                });

            var controller = CreateHousesController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/houses");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/houses"));

            IEnumerable<House> houses;
            var result = await controller.Get(hasAncestralWeapons: true);
            result.TryGetContentValue(out houses);
            var house = houses.ElementAt(0);

            Assert.IsNotNull(houses);
            Assert.AreEqual(1, houses.Count());
            Assert.AreEqual("houseTwo", house.Name);
        }

        private static UrlHelper CreateUrlHelper(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: "BooksApi",
                routeTemplate: "api/books/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            configuration.Routes.MapHttpRoute(
               name: "CharactersApi",
               routeTemplate: "api/characters/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            configuration.Routes.MapHttpRoute(
               name: "HousesApi",
               routeTemplate: "api/houses/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);

            var urlHelper = new UrlHelper(requestMessage);

            return urlHelper;
        }

        private void SeedDatabase(params HouseEntity[] books)
        {
            DbContext.Houses.AddRange(books);
            DbContext.SaveChanges();
        }

        private HousesController CreateHousesController()
        {
            var cacheSettings = MockRepository.GenerateMock<ISecondLevelCacheSettings>();
            cacheSettings.Stub(x => x.ShouldUseSecondLevelCache).Return(false);

            return new HousesController(new HouseService(new EntityFrameworkRepository<HouseEntity, int>(DbContext, cacheSettings)), new HouseMapper(),
                new HousePagingLinksFactory());
        }
    }
}