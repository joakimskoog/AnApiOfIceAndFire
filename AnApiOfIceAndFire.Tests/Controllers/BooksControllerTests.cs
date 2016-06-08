using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Books;
using AnApiOfIceAndFire.Infrastructure.Links;
using AnApiOfIceAndFire.Models.v1;
using AnApiOfIceAndFire.Models.v1.Mappers;
using Geymsla.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using MediaType = AnApiOfIceAndFire.Domain.Books.MediaType;

namespace AnApiOfIceAndFire.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BooksControllerTests
    {
        [TestMethod]
        public async Task GivenThatBookWithGivenIdDoesNotExist_WhenTryingToGetBook_ThenResultIsOfTypeNotFound()
        {
            var service = MockRepository.GenerateMock<IModelService<IBook, BookFilter>>();
            service.Stub(x => x.GetAsync(Arg<int>.Is.Anything)).Return(Task.FromResult((IBook)null));
            BooksController controller = new BooksController(service, MockRepository.GenerateMock<IModelMapper<IBook, Book>>(), MockRepository.GenerateMock<IPagingLinksFactory<BookFilter>>());

            var result = await controller.Get(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GivenThatBookWithGivenIdExists_WhenTryingToGetBook_ThenResultContainsBook()
        {
            var service = MockRepository.GenerateMock<IModelService<IBook, BookFilter>>();
            var book = MockRepository.GenerateMock<IBook>();
            book.Stub(x => x.Name).Return("book");
            book.Stub(x => x.Identifier).Return(1);
            book.Stub(x => x.Country).Return("country");
            book.Stub(x => x.ISBN).Return("isbn");
            book.Stub(x => x.Authors).Return(new List<string> { "author" });
            book.Stub(x => x.Released).Return(new DateTime(2000, 1, 1));
            book.Stub(x => x.Publisher).Return("publisher");
            book.Stub(x => x.MediaType).Return(MediaType.Hardcover);
            book.Stub(x => x.NumberOfPages).Return(10);

            service.Stub(x => x.GetAsync(Arg<int>.Is.Equal(1))).Return(Task.FromResult(book));
            var mapper = MockRepository.GenerateMock<IModelMapper<IBook, Book>>();
            mapper.Stub(x => x.Map(Arg<IBook>.Matches(b => b.Identifier == 1), Arg<UrlHelper>.Is.Anything))
                .Return(new Book("someKindOfUrl/1", book.Name, book.ISBN, book.Authors, book.NumberOfPages,
                    book.Publisher, book.Country,
                    AnApiOfIceAndFire.Models.v1.MediaType.Hardcover, book.Released, new List<string>(), new List<string>()));
            BooksController controller = new BooksController(service, mapper, MockRepository.GenerateMock<IPagingLinksFactory<BookFilter>>());


            var result = await controller.Get(1);
            var okResult = result as OkNegotiatedContentResult<Book>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(book.Name, okResult.Content.Name);
            Assert.AreEqual("someKindOfUrl/1", okResult.Content.URL);
        }

        [TestMethod]
        public async Task GivenThatNoBooksExists_WhenTryingTogetAll_ThenResultContainsNoBooks()
        {
            var service = MockRepository.GenerateMock<IModelService<IBook, BookFilter>>();
            var bookPagedList = MockRepository.GenerateMock<IPagedList<IBook>>();

            service.Stub(x => x.GetPaginatedAsync(0, 0, null)).IgnoreArguments()
                .Return(Task.FromResult((IPagedList<IBook>)new PagedList<IBook>(Enumerable.Empty<IBook>().AsQueryable(), 1, 1)));
            var mapper = MockRepository.GenerateMock<IModelMapper<IBook, Book>>();
            mapper.Stub(x => x.Map(null, null)).IgnoreArguments().Return(null);
            var linksFactory = MockRepository.GenerateMock<IPagingLinksFactory<BookFilter>>();
            linksFactory.Stub(x => x.Create(bookPagedList, null, new BookFilter())).IgnoreArguments().Return(new[] { new Link(new Uri("https://localhost.com")) });

            var controller = new BooksController(service, mapper, linksFactory)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://localhost.com/something")),
            };

            IEnumerable<Book> books;
            var result = await controller.Get();
            result.TryGetContentValue(out books);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public async Task GivenThatOneBookExists_WhenTryingToGetAll_ThenResultContainsCorrectBook()
        {
            var book = CreateMockedBook(1);
            var service = MockRepository.GenerateMock<IModelService<IBook, BookFilter>>();
            var pagedList = new PagedList<IBook>(new List<IBook> { book }.AsQueryable(), 1, 1);

            service.Stub(x => x.GetPaginatedAsync(0, 0)).IgnoreArguments()
                .Return(Task.FromResult((IPagedList<IBook>)pagedList));
            var linksFactory = MockRepository.GenerateMock<IPagingLinksFactory<BookFilter>>();
            linksFactory.Stub(x => x.Create(pagedList, null, new BookFilter())).IgnoreArguments().Return(new[] { new Link(new Uri("https://localhost.com")) });
            var mapper = new BookMapper(new MediaTypeMapper());
            var controller = new BooksController(service, mapper, linksFactory)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost.com")),
                Url = Helper.CreateUrlHelper("http://localhost.com")
            };


            IEnumerable<Book> books;
            var result = await controller.Get();
            result.TryGetContentValue(out books);

            Assert.AreEqual(1, books.Count());
        }

        private static IBook CreateMockedBook(int id, string name = "bookName", string isbn = "isbn", int numberOfPages = 100, string publisher = "publisher",
            string country = "country", MediaType mediaType = Domain.Books.MediaType.Hardcover)
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
    }
}