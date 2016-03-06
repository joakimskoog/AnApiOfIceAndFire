using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace AnApiOfIceAndFire.Tests.Models.v0.Mappers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CharacterMapperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatCharacterIsNull_WhenTryingToMapIt_ThenArgumentNullExceptionIsThrown()
        {
            var urlHelper = CreateUrlHelper("http://localhost/api/characters/1");
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(null, urlHelper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatUrlHelperIsNull_WhenTryingToMapCharacter_ThenArgumentNullExceptionIsThrown()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, null);
        }

        [TestMethod]
        public void GivenThatCharacterHasIdentifierOfOne_WhenTryingToMapCharacter_ThenMappedCharacterUrlIsCorrectWithIdentifierInIt()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual("http://localhost/api/characters/1", mappedCharacter.URL);
        }

        [TestMethod]
        public void GivenThatCharacterHasFather_WhenTryingToMapCharacter_ThenMappedCharacterFatherContainsCorrectUrl()
        {
            var father = MockRepository.GenerateMock<ICharacter>();
            father.Stub(x => x.Identifier).Return(2);
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Father).Return(father);
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual($"http://localhost/api/characters/{character.Father.Identifier}", mappedCharacter.Father);
        }

        [TestMethod]
        public void GivenThatCharacterHasMother_WhenTryingToMapCharacter_ThenMappedCharacterMotherContainsCorrectUrl()
        {
            var mother = MockRepository.GenerateMock<ICharacter>();
            mother.Stub(x => x.Identifier).Return(1000);
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Mother).Return(mother);
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual($"http://localhost/api/characters/{character.Mother.Identifier}", mappedCharacter.Mother);
        }

        [TestMethod]
        public void GivenThatCharacterHasSpouse_WhenTryingToMapCharacter_ThenMappedCharacterSpouseContainsCorrectUrl()
        {
            var spouse = MockRepository.GenerateMock<ICharacter>();
            spouse.Stub(x => x.Identifier).Return(2);
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Spouse).Return(spouse);
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual($"http://localhost/api/characters/{character.Spouse.Identifier}", mappedCharacter.Spouse);
        }

        [TestMethod]
        public void GivenThatCharacterHasAllegiance_WhenTryingToMapCharacter_ThenMappedCharacterAllegiancesContainsCorrectUrls()
        {
            var house = MockRepository.GenerateMock<IHouse>();
            house.Stub(x => x.Identifier).Return(1);
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Allegiances).Return(new List<IHouse> { house });
            character.Stub(x => x.Identifier).Return(1);
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));


            Assert.AreEqual(1, mappedCharacter.Allegiances.Count());
            Assert.AreEqual("http://localhost/api/houses/1", mappedCharacter.Allegiances.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatCharacterHasBooks_WhenTryingToMapCharacter_ThenMappedCharacterBooksContainsCorrectUrls()
        {
            var book = MockRepository.GenerateMock<IBook>();
            book.Stub(x => x.Identifier).Return(10);
            var characer = MockRepository.GenerateMock<ICharacter>();
            characer.Stub(x => x.Identifier).Return(1);
            characer.Stub(x => x.Books).Return(new List<IBook> { book });
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(characer, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(1, mappedCharacter.Books.Count());
            Assert.AreEqual("http://localhost/api/books/10", mappedCharacter.Books.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatCharacterHasPovBooks_WhenTryingToMapCharacter_ThenMappedCharacterPovBooksContainsCorrectUrls()
        {
            var book = MockRepository.GenerateMock<IBook>();
            book.Stub(x => x.Identifier).Return(10);
            var characer = MockRepository.GenerateMock<ICharacter>();
            characer.Stub(x => x.Identifier).Return(1);
            characer.Stub(x => x.PovBooks).Return(new List<IBook> { book });
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(characer, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(1, mappedCharacter.PovBooks.Count());
            Assert.AreEqual("http://localhost/api/books/10", mappedCharacter.PovBooks.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatCharacterHasName_WhenTryingToMapCharacter_ThenMappedCharacterHasSameName()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Name).Return("testCharacterName");
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.Name, mappedCharacter.Name);
        }

        [TestMethod]
        public void GivenThatCharacterHasCulture_WhenTryingToMapCharacter_ThenMappedCharacterHasSameCulture()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Culture).Return("testCulture");
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.Culture, mappedCharacter.Culture);
        }

        [TestMethod]
        public void GivenThatCharacterWasBorn_WhenTryingToMapCharacter_ThenMappedCharacterHasSameBornData()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Born).Return("someYear");
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.Born, mappedCharacter.Born);
        }

        [TestMethod]
        public void GivenThatCharacterIsDead_WhenTryingToMapCharacter_ThenMappedCharacterHasSameDiedData()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Died).Return("someYear");
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.Died, mappedCharacter.Died);
        }

        [TestMethod]
        public void GivenThatCharacterHasTitles_WhenTryingToMapCharacter_ThenMappedCharacterHasSameTitles()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Titles).Return(new List<string> { "firstTitle", "secondTitle" });
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.Titles.Count, mappedCharacter.Titles.Count());
            Assert.AreEqual(character.Titles.ElementAt(0), mappedCharacter.Titles.ElementAt(0));
            Assert.AreEqual(character.Titles.ElementAt(1), mappedCharacter.Titles.ElementAt(1));
        }

        [TestMethod]
        public void GivenThatCharacterHasAliases_WhenTryingToMapCharacter_ThenMappedCharacterHasSameAliases()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.Aliases).Return(new List<string> { "firstAlias", });
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.Aliases.Count, mappedCharacter.Aliases.Count());
            Assert.AreEqual(character.Aliases.ElementAt(0), mappedCharacter.Aliases.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatCharacterHasTvSeries_WhenTryingToMapCharacter_ThenMappedCharacterHasSameTvSeries()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.TvSeries).Return(new List<string> { "Season 1", });
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.TvSeries.Count, mappedCharacter.TvSeries.Count());
            Assert.AreEqual(character.TvSeries.ElementAt(0), mappedCharacter.TvSeries.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatCharacterHasPlayedBy_WhenTryingToMapCharacter_ThenMappedCharacterHasSamePlayedBy()
        {
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(1);
            character.Stub(x => x.PlayedBy).Return(new List<string> { "Some Actor", });
            var mapper = new CharacterMapper(new GenderMapper());

            var mappedCharacter = mapper.Map(character, CreateUrlHelper("http://localhost/api/characters/1"));

            Assert.AreEqual(character.PlayedBy.Count, mappedCharacter.PlayedBy.Count());
            Assert.AreEqual(character.PlayedBy.ElementAt(0), mappedCharacter.PlayedBy.ElementAt(0));
        }

        private UrlHelper CreateUrlHelper(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: BookLinkCreator.BookRouteName,
                routeTemplate: "api/books/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            configuration.Routes.MapHttpRoute(
               name: CharacterLinkCreator.CharacterRouteName,
               routeTemplate: "api/characters/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            configuration.Routes.MapHttpRoute(
               name: HouseLinkCreator.HouseRouteName,
               routeTemplate: "api/houses/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);

            var urlHelper = new UrlHelper(requestMessage);

            return urlHelper;
        }
    }
}