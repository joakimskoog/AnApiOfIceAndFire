using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using AnApiOfIceAndFire.Controllers;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using MediaType = AnApiOfIceAndFire.Domain.Models.MediaType;

namespace AnApiOfIceAndFire.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatBookServiceIsNull_WhenConstructingBooksController_ThenArgumentNullExceptionIsThrown()
        {
            var controller = new BooksController(null);
        }

        [TestMethod]
        public void GivenThatBookWithGivenIdDoesNotExist_WhenTryingToGetBook_ThenResultIsOfTypeNotFound()
        {
            var service = MockRepository.GenerateMock<IBookService>();
            service.Stub(x => x.GetBook(Arg<int>.Is.Anything)).Return(null);
            var controller = new BooksController(service);

            var result = controller.Get(0);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GivenThatBookWithGivenIdExists_WhenTryingToGetBook_ThenResultContainsExistingBook()
        {
            var service = MockRepository.GenerateMock<IBookService>();
            var dummyBook = (new DummyBook()
            {
                Name = "TestBook",
                Country = "Sweden",
            });
            service.Stub(x => x.GetBook(Arg<int>.Is.Equal(1))).Return(dummyBook);
            var controller = new BooksController(service);

            var result = controller.Get(1) as OkNegotiatedContentResult<Book>;

            Assert.AreEqual(dummyBook.Name, result.Content.Name);
            Assert.AreEqual(dummyBook.Country, result.Content.Country);
        }
    }

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