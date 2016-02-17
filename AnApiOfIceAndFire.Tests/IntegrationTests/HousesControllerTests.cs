using System;
using System.Data.Entity;
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
            SeedDatabase(new HouseEntity()
            {
                Id = 1,
                Name = "houseOne",
                AncestralWeapons = new[] {"coolWeapon"},
                Seats = new[] {"seatOne"},
                Titles = new[] {"titleOne"}
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