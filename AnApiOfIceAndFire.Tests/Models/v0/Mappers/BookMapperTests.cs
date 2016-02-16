using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v1.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace AnApiOfIceAndFire.Tests.Models.v0.Mappers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BookMapperTests
    {
        private const string RequestedUri = "http://localhost/api/books/1";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatBookIsNull_WhenTryingToMapIt_ThenArgumentNullExceptionIsThrown()
        {
            var urlHelper = CreateUrlHelper(RequestedUri);
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(null, urlHelper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatUrlHelperIsNull_WhenTryingToMapBook_ThenArgumentNullExceptionIsThrown()
        {
            var book = MockRepository.GenerateMock<IBook>();
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, null);
        }

        [TestMethod]
        public void GivenThatBookHasIdentifier_WhenTryingToMapIt_ThenMappedBookHasUrlWithIdentifierInIt()
        {
            var book = CreateMockedBook(1);
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(RequestedUri, mappedBook.URL);
        }

        [TestMethod]
        public void GivenThatBookHasCharacters_WhenTryingToMapIt_ThenMappedBookCharactersContainsCorrectUrls()
        {
            var book = CreateMockedBook(1);
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(10);
            book.Stub(x => x.Characters).Return(new List<ICharacter> { character });
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(1, mappedBook.Characters.Count());
            Assert.AreEqual($"http://localhost/api/characters/{character.Identifier}", mappedBook.Characters.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatBookHasPovCharacters_WhenTryingToMapIt_ThenMappedBookPovCharactersContainsCorrectUrls()
        {
            var book = CreateMockedBook(1);
            var character = MockRepository.GenerateMock<ICharacter>();
            character.Stub(x => x.Identifier).Return(10);
            book.Stub(x => x.POVCharacters).Return(new List<ICharacter> { character });
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(1, mappedBook.POVCharacters.Count());
            Assert.AreEqual($"http://localhost/api/characters/{character.Identifier}", mappedBook.POVCharacters.ElementAt(0));
        }

        [TestMethod]
        public void GivenThatBookHasName_WhenTryingToMapIt_ThenMappedBookHasSameName()
        {
            var book = CreateMockedBook(1);
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.Name, mappedBook.Name);
        }

        [TestMethod]
        public void GivenThatBookHasISBN_WhenTryingToMapIt_ThenMappedBookHasSameISBN()
        {
            var book = CreateMockedBook(1);
            book.Stub(x => x.ISBN).Return("isbn");
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.ISBN, mappedBook.ISBN);
        }

        [TestMethod]
        public void GivenThatBookHasNumberOfPages_WhenTryingToMapIt_ThenMappedBookHasSameNumberOfPages()
        {
            var book = CreateMockedBook(1);
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.NumberOfPages, mappedBook.NumberOfPages);
        }

        [TestMethod]
        public void GivenThatBookHasPubliser_WhenTryingToMapIt_ThenMappedBookHasSamePublisher()
        {
            var book = CreateMockedBook(1);
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.Publisher, mappedBook.Publisher);
        }

        [TestMethod]
        public void GivenThatBookHasCountry_WhenTryingToMapIt_ThenMappedBookHasSameCountry()
        {
            var book = CreateMockedBook(1);
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.Country, mappedBook.Country);
        }

        [TestMethod]
        public void GivenThatBookHasReleaseDate_WhenTryingToMapIt_ThenMappedBookHasSameReleaseDate()
        {
            var book = CreateMockedBook(1);
            book.Stub(x => x.Released).Return(new DateTime(2000, 1, 1));
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.Released, mappedBook.Released);
        }

        [TestMethod]
        public void GivenThatBookHasMediaType_WhenTryingToMapIt_ThenMappedBookHasSameMediaType()
        {
            var book = CreateMockedBook(1);
            book.Stub(x => x.Released).Return(new DateTime(2000, 1, 1));
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(AnApiOfIceAndFire.Models.v1.MediaType.Hardcover, mappedBook.MediaType);
        }

        [TestMethod]
        public void GivenThatBookHasAuthors_WhenTryingToMapIt_ThenMappedBookHasSameAuthors()
        {
            var book = CreateMockedBook(1);
            book.Stub(x => x.Released).Return(new DateTime(2000, 1, 1));
            var mapper = new BookMapper(new MediaTypeMapper());

            var mappedBook = mapper.Map(book, CreateUrlHelper(RequestedUri));

            Assert.AreEqual(book.Authors.Count, mappedBook.Authors.Count());
            Assert.AreEqual(book.Authors.ElementAt(0), mappedBook.Authors.ElementAt(0));
        }

        private static IBook CreateMockedBook(int id, string name = "bookName", string isbn = "isbn", int numberOfPages = 100, string publisher = "publisher",
            string country = "country", MediaType mediaType = MediaType.Hardcover)
        {
            var book = MockRepository.GenerateMock<IBook>();
            book.Stub(x => x.Identifier).Return(id);
            book.Stub(x => x.Name).Return(name);
            book.Stub(x => x.ISBN).Return(isbn);
            book.Stub(x => x.NumberOfPages).Return(numberOfPages);
            book.Stub(x => x.Publisher).Return(publisher);
            book.Stub(x => x.Country).Return(country);
            book.Stub(x => x.MediaType).Return(mediaType);
            book.Stub(x => x.Released).Return(new DateTime(2000, 1, 1));
            book.Stub(x => x.Authors).Return(new List<string> { "authorOne" });

            return book;
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
    }
}