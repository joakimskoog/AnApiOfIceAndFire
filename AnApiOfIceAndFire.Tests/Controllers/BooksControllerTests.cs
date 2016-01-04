using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Controllers.v0;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;
using AnApiOfIceAndFire.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using SimplePagination;
using MediaType = AnApiOfIceAndFire.Domain.Models.MediaType;

namespace AnApiOfIceAndFire.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BooksControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatBookServiceIsNull_WhenConstructingBooksController_ThenArgumentNullExceptionIsThrown()
        {
            var controller = new BooksController(null, MockRepository.GenerateMock<IModelMapper<IBook, Book>>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatModelMapperIsNull_WhenConstructingBooksController_ThenArgumentNullExceptionIsThrown()
        {
            var controller = new BooksController(MockRepository.GenerateMock<IModelService<IBook>>(), null);
        }

        [TestMethod]
        public void GivenThatBookWithGivenIdDoesNotExist_WhenTryingToGetBook_ThenResultIsOfTypeNotFound()
        {
            var service = MockRepository.GenerateMock<IModelService<IBook>>();
            service.Stub(x => x.Get(Arg<int>.Is.Anything)).Return(null);
            var controller = new BooksController(service, MockRepository.GenerateMock<IModelMapper<IBook, Book>>());

            var result = controller.Get(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GivenThatBookWithGivenIdExists_WhenTryingToGetBook_ThenResultContainsBook()
        {
            var service = MockRepository.GenerateMock<IModelService<IBook>>();
            var book = new DummyBook()
            {
                Name = "nameOfBook",
                Identifier = 1,
                Country = "country",
                ISBN = "isbn",
                Authors = new List<string> { "author" },
                Released = new DateTime(2000, 1, 1),
                Publisher = "publisher",
                MediaType = MediaType.Hardcover,
                NumberOfPages = 10,

            };
            service.Stub(x => x.Get(Arg<int>.Is.Equal(1))).Return(book);
            var mapper = MockRepository.GenerateMock<IModelMapper<IBook, Book>>();
            mapper.Stub(x => x.Map(Arg<IBook>.Matches(b => b.Identifier == 1), Arg<UrlHelper>.Is.Anything))
                .Return(new Book("someKindOfUrl/1", book.Name, book.ISBN, book.Authors, book.NumberOfPages,
                    book.Publisher, book.Country,
                    Models.v0.MediaType.Hardcover, book.Released, new List<string>(), new List<string>()));
            var controller = new BooksController(service, mapper);


            var result = controller.Get(1) as OkNegotiatedContentResult<Book>;

            Assert.IsNotNull(result);
            Assert.AreEqual(book.Name, result.Content.Name);
            Assert.AreEqual("someKindOfUrl/1", result.Content.URL);
        }

        [TestMethod]
        public void GivenThatNoBooksExists_WhenTryingTogetAll_ThenResultContainsNoBooks()
        {
            var service = MockRepository.GenerateMock<IModelService<IBook>>();
            service.Stub(x => x.GetPaginated(Arg<int>.Is.Anything, Arg<int>.Is.Anything))
                .Return(new PagedList<IBook>(new List<IBook>().AsQueryable(), 1, 1));
            var mapper = MockRepository.GenerateMock<IModelMapper<IBook,Book>>();
            var urlHelper = MockRepository.GenerateMock<UrlHelper>();
            urlHelper.Stub(x => x.Link(Arg<string>.Is.Anything, Arg<object>.Is.Anything)).Return("https://localhost.com");
            var controller = new BooksController(service, mapper)
            {
                Configuration = new HttpConfiguration(),
                Url = urlHelper,
                Request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://localhost.com/something"))
            };

            IEnumerable<Book> books;
            var result = controller.Get();
            result.TryGetContentValue(out books);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(0, books.Count());
        }

        //    [TestMethod]
        //    public void GivenThatOneBookExists_WhenTryingToGetAllWithPageOneAndPageSizeOfTen_ThenExistingBookIsReturned()
        //    {
        //        var service = MockRepository.GenerateMock<IBookService>();
        //        var dummyBook = new DummyBook
        //        {
        //            Name = "FirstBook",
        //            Country = "Sweden",
        //            Identifier = 1,
        //            ISBN = "isbn",
        //            Authors = new List<string>() { "author1" },
        //            Released = new DateTime(2000, 1, 1),
        //            NumberOfPages = 10,
        //            Publisher = "publisher"
        //        };
        //        service.Stub(x => x.GetPaginatedBooks(Arg<int>.Is.Equal(1), Arg<int>.Is.Equal(10)))
        //            .Return(new PagedList<IBook>(new List<IBook> { dummyBook }.AsQueryable(), 1, 10));
        //        var urlHelper = MockRepository.GenerateMock<UrlHelper>();
        //        urlHelper.Stub(x => x.Link(Arg<string>.Is.Anything, Arg<object>.Is.Anything)).Return("https://localhost.com");
        //        var controller = new BooksController(service)
        //        {
        //            Url = urlHelper
        //        };

        //        var result = controller.Get(page: 1, pageSize: 10) as LinkHeaderResult;

        //        result.

        //        Assert.IsNotNull(result);
        //        // Assert.AreEqual(1, result.Content.Count());
        //    }

        //    [TestMethod]
        //    public void GivenThatNoBooksExists_WhenTryingToGetAll_ThenResultIsEmpty()
        //    {
        //        var service = MockRepository.GenerateMock<IBookService>();
        //        service.Stub(x => x.GetAllBooks()).Return(Enumerable.Empty<IBook>().AsQueryable());
        //        service.Stub(x => x.GetPaginatedBooks(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Return(new PagedList<IBook>(Enumerable.Empty<IBook>().AsQueryable(), 1, 1));
        //        var urlHelper = MockRepository.GenerateMock<UrlHelper>();
        //        urlHelper.Stub(x => x.Link(Arg<string>.Is.Anything, Arg<object>.Is.Anything)).Return("https://localhost.com");
        //        var controller = new BooksController(service)
        //        {
        //            Url = urlHelper
        //        };

        //        var result = controller.Get(page: 1, pageSize: 10) as OkNegotiatedContentResult<IEnumerable<Book>>;

        //        Assert.AreEqual(0, result.Content.Count());
        //    }
    }

    [ExcludeFromCodeCoverage]
    public class DummyBook : IBook
    {
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public ICollection<string> Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public MediaType MediaType { get; set; }
        public DateTime Released { get; set; }
        public ICollection<ICharacter> Characters { get; set; }
        public ICollection<ICharacter> POVCharacters { get; set; }
    }
}