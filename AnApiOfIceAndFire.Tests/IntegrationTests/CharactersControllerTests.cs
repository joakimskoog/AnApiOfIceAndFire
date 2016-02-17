using System;
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
    public class CharactersControllerTests : DbIntegrationTestHarness
    {
        [TestMethod]
        public async Task GivenThatNoCharacterExists_WhenTryingToGetCharacterWithIdOne_ThenNoCharacterIsReturnedAndResponseIsNotFound()
        {
            var controller = CreateCharactersController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/characters/1");
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Get(id: 1);
            var characterResult = response as NotFoundResult;

            Assert.IsNotNull(characterResult);
        }

        [TestMethod]
        public async Task GivenThatCharacterWithIdOneExists_WhenTryingToGetCharacterWithIdOne_ThenCharacterWithIdOneIsReturned()
        {
            SeedDatabase(new CharacterEntity()
            {
                Id  = 1,
                Name = "characterOne",
                Aliases = new[] {"aliasOne"},
                PlayedBy = new[] {"actorOne"},
                Titles = new[] {"titleOne"},
                TvSeries = new[] {"seriesOne"} 
            });
            var controller = CreateCharactersController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/characters/1");
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Get(id: 1);
            var characterResult = (response as OkNegotiatedContentResult<Character>).Content;

            Assert.IsNotNull(characterResult);
            Assert.AreEqual("characterOne", characterResult.Name);
            Assert.AreEqual("http://localhost.com/api/characters/1", characterResult.URL);
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

        private void SeedDatabase(params CharacterEntity[] characters)
        {
            DbContext.Characters.AddRange(characters);
            DbContext.SaveChanges();
        }

        private CharactersController CreateCharactersController()
        {
            var cacheSettings = MockRepository.GenerateMock<ISecondLevelCacheSettings>();
            cacheSettings.Stub(x => x.ShouldUseSecondLevelCache).Return(false);

            return new CharactersController(new CharacterService(new EntityFrameworkRepository<CharacterEntity, int>(DbContext, cacheSettings)), new CharacterMapper(), 
                new CharacterPagingLinksFactory());
        }
    }
}