using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace AnApiOfIceAndFire.Domain.Tests.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BookServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatRepositoryIsNull_WhenCreatingService_ThenArgumentNullExceptionIsThrown()
        {
            var service = new BookService(null);
        }

        [TestMethod]
        public void GivenThatNoBookExists_WhenTryingToGetById_ThenReturnedBookIsNull()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetById(0)).IgnoreArguments().Return(null);
            var service = new BookService(repository);

            var book = service.Get(1);

            Assert.IsNull(book);
        }

        [TestMethod]
        public void GivenThatBookWithGivenIdExists_WhenTryingToGetById_ThenReturnedBookHasSameId()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.Get(1, null)).IgnoreArguments().Return(new BookEntity
            {
                Id = 1
            });
            var service = new BookService(repository);

            var book = service.Get(1);

            Assert.IsNotNull(book);
            Assert.AreEqual(1, book.Identifier);
        }

        [TestMethod]
        public void GivenThatRepositoryReturnsNull_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(null);
            var service = new BookService(repository);

            var books = service.GetAll();

            Assert.IsNotNull(books);
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public void GivenThatNoBookExists_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(Enumerable.Empty<BookEntity>().AsQueryable());
            var service = new BookService(repository);

            var books = service.GetAll();

            Assert.IsNotNull(books);
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public void GivenThatOneBookExists_WhenTryingToGetAll_ThenReturnedListContainsOneBook()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(new List<BookEntity> { new BookEntity() { Id = 1 } }.AsQueryable());
            var service = new BookService(repository);

            var books = service.GetAll();

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
        }

        [TestMethod]
        public void GivenThatTenBooksExists_WhenTryingToGetAll_ThenReturnedListContainsTenBooks()
        {
            var booksToReturn = new List<BookEntity>();
            for (int i = 0; i < 10; i++)
            {
                booksToReturn.Add(new BookEntity() { Id = i });
            }
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetAll())
                .IgnoreArguments()
                .Return(booksToReturn.AsQueryable());
            var service = new BookService(repository);

            var books = service.GetAll();

            Assert.IsNotNull(books);
            Assert.AreEqual(10, books.Count());
        }

        [TestMethod]
        public void GivenThatRepositoryReturnsNull_WhenTryingToGetPaginated_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(null);
            var service = new BookService(repository);

            var books = service.GetPaginated(1,10);

            Assert.IsNotNull(books);
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public void GivenThatNoBooksExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<BookEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(Enumerable.Empty<BookEntity>().AsQueryable());
            var service = new BookService(repository);

            var paginatedBooks = service.GetPaginated(1, 10);

            Assert.IsNotNull(paginatedBooks);
            Assert.AreEqual(0, paginatedBooks.Count);
        }
    }
}