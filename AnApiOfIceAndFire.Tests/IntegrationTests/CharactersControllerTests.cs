using System;
using System.Collections.Generic;
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
                Id = 1,
                Name = "characterOne",
                Aliases = new[] { "aliasOne" },
                PlayedBy = new[] { "actorOne" },
                Titles = new[] { "titleOne" },
                TvSeries = new[] { "seriesOne" }
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

        [TestMethod]
        public async Task GivenThatThreeCharactersExistsAndNoFilterParameterInRequest_WhenTryingToGetCharacters_ThenAllCharactersAreReturned()
        {
            SeedDatabase(new CharacterEntity()
            {
                Id = 1,
                Name = "characterOne",
                Aliases = new[] { "aliasOne" },
                PlayedBy = new[] { "actorOne" },
                Titles = new[] { "titleOne" },
                TvSeries = new[] { "seriesOne" }
            },
            new CharacterEntity()
            {
                Id = 2,
                Name = "characterTwo",
                Aliases = new[] { "aliasOne" },
                PlayedBy = new[] { "actorOne" },
                Titles = new[] { "titleOne" },
                TvSeries = new[] { "seriesOne" }
            },
            new CharacterEntity()
            {
                Id = 3,
                Name = "characterThree",
                Aliases = new[] { "aliasOne" },
                PlayedBy = new[] { "actorOne" },
                Titles = new[] { "titleOne" },
                TvSeries = new[] { "seriesOne" }
            });

            var controller = CreateCharactersController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/characters");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/characters"));

            IEnumerable<Character> characters;
            var result = await controller.Get();
            result.TryGetContentValue(out characters);

            Assert.IsNotNull(characters);
            Assert.AreEqual(3, characters.Count());
        }

        [TestMethod]
        public async Task GivenTwoBooksAndOneMatchingNameFilterParameter_WhenTryingToGetCharacters_ThenOneCharacterIsReturned()
        {
            SeedDatabase(
                new CharacterEntity()
                {
                    Id = 1,
                    Name = "characterOne",
                    Aliases = new[] { "aliasOne" },
                    PlayedBy = new[] { "actorOne" },
                    Titles = new[] { "titleOne" },
                    TvSeries = new[] { "seriesOne" }
                },
                new CharacterEntity()
                {
                    Id = 2,
                    Name = "characterTwo",
                    Aliases = new[] { "aliasOne" },
                    PlayedBy = new[] { "actorOne" },
                    Titles = new[] { "titleOne" },
                    TvSeries = new[] { "seriesOne" }
                });

            var controller = CreateCharactersController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/characters");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/characters"));

            IEnumerable<Character> characters;
            var result = await controller.Get(name: "characterTwo");
            result.TryGetContentValue(out characters);
            var book = characters.ElementAt(0);

            Assert.IsNotNull(characters);
            Assert.AreEqual(1, characters.Count());
            Assert.AreEqual("characterTwo", book.Name);
        }

        [TestMethod]
        public async Task GivenTwoCharactersOneMatchingCultureFilterParameter_WhenTryingToGetCharacters_ThenOneCharacterIsReturned()
        {
            SeedDatabase(
                new CharacterEntity()
                {
                    Id = 1,
                    Name = "characterOne",
                    Aliases = new[] { "aliasOne" },
                    PlayedBy = new[] { "actorOne" },
                    Titles = new[] { "titleOne" },
                    TvSeries = new[] { "seriesOne" },
                    Culture = "cultureOne"
                },
                new CharacterEntity()
                {
                    Id = 2,
                    Name = "characterTwo",
                    Aliases = new[] { "aliasOne" },
                    PlayedBy = new[] { "actorOne" },
                    Titles = new[] { "titleOne" },
                    TvSeries = new[] { "seriesOne" },
                    Culture = "cultureTwo"
                });

            var controller = CreateCharactersController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/characters");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/characters"));

            IEnumerable<Character> characters;
            var result = await controller.Get(culture:"cultureTwo");
            result.TryGetContentValue(out characters);
            var book = characters.ElementAt(0);

            Assert.IsNotNull(characters);
            Assert.AreEqual(1, characters.Count());
            Assert.AreEqual("characterTwo", book.Name);
        }

        [TestMethod]
        public async Task GivenTwoCharactersWithOneMatchingBornFilterParameter_WhenTryingToGetCharacters_ThenOneCharacterIsReturned()
        {
            SeedDatabase(
                new CharacterEntity()
                {
                    Id = 1,
                    Name = "characterOne",
                    Aliases = new[] { "aliasOne" },
                    PlayedBy = new[] { "actorOne" },
                    Titles = new[] { "titleOne" },
                    TvSeries = new[] { "seriesOne" },
                    Culture = "cultureOne",
                    Born = "200 AC"
                },
                new CharacterEntity()
                {
                    Id = 2,
                    Name = "characterTwo",
                    Aliases = new[] { "aliasOne" },
                    PlayedBy = new[] { "actorOne" },
                    Titles = new[] { "titleOne" },
                    TvSeries = new[] { "seriesOne" },
                    Culture = "cultureTwo",
                    Born = "201 AC"
                });

            var controller = CreateCharactersController();
            controller.Url = CreateUrlHelper("http://localhost.com/api/characters");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/characters"));

            IEnumerable<Character> characters;
            var result = await controller.Get(born: "201 AC");
            result.TryGetContentValue(out characters);
            var book = characters.ElementAt(0);

            Assert.IsNotNull(characters);
            Assert.AreEqual(1, characters.Count());
            Assert.AreEqual("characterTwo", book.Name);
        }

        
        
        //public string Died { get; set; }
        //public bool? IsAlive { get; set; }

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