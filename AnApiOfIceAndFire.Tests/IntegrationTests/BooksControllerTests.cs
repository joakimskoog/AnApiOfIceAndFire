using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Books;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;
using Geymsla.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
// ReSharper disable PossibleNullReferenceException

namespace AnApiOfIceAndFire.Tests.IntegrationTests
{
    [TestClass]
    public class BooksControllerTests : DbIntegrationTestHarness
    {
        [TestMethod]
        public async Task GivenThatNoBookExists_WhenTryingToGetBookWithidOne_ThenNoBookIsReturnedAndResponseIsNotFound()
        {
            var controller = CreateBooksController();
            controller.Url = Helper.CreateUrlHelper("http://localhost.com/api/books/1");
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Get(id: 1);
            var bookResult = response as NotFoundResult;

            Assert.IsNotNull(bookResult);
        }

        [TestMethod]
        public async Task GivenThatBookWithIdOneExists_WhenTryingToGetBookWithIdOne_ThenBookWithIdOneIsReturned()
        {
            SeedDatabase(new BookEntity
            {
                Id = 1,
                Name = "bookOne",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            });
            var controller = CreateBooksController();
            controller.Url = Helper.CreateUrlHelper("http://localhost.com/api/books/1");
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Get(id: 1);
            var bookResult = (response as OkNegotiatedContentResult<Book>).Content;

            Assert.IsNotNull(bookResult);
            Assert.AreEqual("bookOne", bookResult.Name);
            Assert.AreEqual("http://localhost.com/api/books/1", bookResult.URL);
        }

        [TestMethod]
        public async Task GivenThatThreeBooksExistsAndNoFilterParameterInRequest_WhenTryingToGetBooks_ThenAllBooksAreReturned()
        {
            SeedDatabase(new BookEntity
            {
                Id = 1,
                Name = "bookOne",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            }, new BookEntity()
            {
                Id = 2,
                Name = "bookTwo",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            }, new BookEntity()
            {
                Id = 3,
                Name = "bookThree",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            });
            var controller = CreateBooksController();
            controller.Url = Helper.CreateUrlHelper("http://localhost.com/api/books");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/books"));

            IEnumerable<Book> books;
            var result = await controller.Get();
            result.TryGetContentValue(out books);

            Assert.IsNotNull(books);
            Assert.AreEqual(3, books.Count());
        }

        [TestMethod]
        public async Task GivenTwoBooksNameFilterParamterMatchingOneBook_WhenTryingToGetBooks_ThenOneBookIsReturned()
        {
            SeedDatabase(new BookEntity
            {
                Id = 1,
                Name = "bookOne",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            }, new BookEntity()
            {
                Id = 2,
                Name = "bookTwo",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            });

            var controller = CreateBooksController();
            controller.Url = Helper.CreateUrlHelper("http://localhost.com/api/books");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/books"));

            IEnumerable<Book> books;
            var result = await controller.Get(name: "bookTwo");
            result.TryGetContentValue(out books);
            var book = books.ElementAt(0);

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("bookTwo", book.Name);
        }

        [TestMethod]
        public async Task GivenTwoBooksWithOneMatchingDateFilterParameters_WhenTryingToGetBooks_ThenOneBookIsReturned()
        {
            SeedDatabase(new BookEntity
            {
                Id = 1,
                Name = "bookOne",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            }, new BookEntity()
            {
                Id = 2,
                Name = "bookTwo",
                ReleaseDate = new DateTime(2005, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            });

            var controller = CreateBooksController();
            controller.Url = Helper.CreateUrlHelper("http://localhost.com/api/books");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/books"));

            IEnumerable<Book> books;
            var result = await controller.Get(fromReleaseDate: new DateTime(2004, 1, 1), toReleaseDate: new DateTime(2005, 12, 12));
            result.TryGetContentValue(out books);
            var book = books.ElementAt(0);

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("bookTwo", book.Name);
        }

        [TestMethod]
        public async Task GivenTwoBooksAndOneBookMatchesFilterParamters_WhenTryingToGetBooks_ThenOneBookIsReturned()
        {
            SeedDatabase(new BookEntity
            {
                Id = 1,
                Name = "bookOne",
                ReleaseDate = new DateTime(2000, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            }, new BookEntity()
            {
                Id = 2,
                Name = "bookTwo",
                ReleaseDate = new DateTime(2005, 1, 1),
                Authors = new[] { "George R.R. Martin" },
                ISBN = "isbn",
                Publisher = "publisher",
                Country = "USA"
            });

            var controller = CreateBooksController();
            controller.Url = Helper.CreateUrlHelper("http://localhost.com/api/books");
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com/api/books"));

            IEnumerable<Book> books;
            var result = await controller.Get(name:"bookTwo", fromReleaseDate: new DateTime(2004, 1, 1), toReleaseDate: new DateTime(2005, 12, 12));
            result.TryGetContentValue(out books);
            var book = books.ElementAt(0);

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("bookTwo", book.Name);
        }
        
        private void SeedDatabase(params BookEntity[] books)
        {
            DbContext.Books.AddRange(books);
            DbContext.SaveChanges();
        }

        private BooksController CreateBooksController()
        {
            var cacheSettings = MockRepository.GenerateMock<ISecondLevelCacheSettings>();
            cacheSettings.Stub(x => x.ShouldUseSecondLevelCache).Return(false);

            return new BooksController(new BookService(new EntityFrameworkRepository<BookEntity, int>(DbContext, cacheSettings)), new BookMapper(new MediaTypeMapper()),
                new BookPagingLinksFactory());
        }
    }
}