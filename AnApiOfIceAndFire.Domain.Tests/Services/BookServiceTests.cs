using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Services;
using Geymsla;
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
        public async Task GivenThatNoBookExists_WhenTryingToGetById_ThenReturnedBookIsNull()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAsync(null,CancellationToken.None, null)).IgnoreArguments().Return(Task.FromResult(Enumerable.Empty<BookEntity>()));
            var service = new BookService(repository);

            var book = await service.GetAsync(1);

            Assert.IsNull(book);
        }

        [TestMethod]
        public async Task GivenThatBookWithGivenIdExists_WhenTryingToGetById_ThenReturnedBookHasSameId()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAsync(null, CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(new List<BookEntity>
                {
                    new BookEntity()
                    {
                        Id = 1
                    }
                }.AsEnumerable()));
            var service = new BookService(repository);

            var book = await service.GetAsync(1);

            Assert.IsNotNull(book);
            Assert.AreEqual(1, book.Identifier);
        }

        [TestMethod]
        public async Task GivenThatRepositoryReturnsNull_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult((IEnumerable<BookEntity>)null));
            var service = new BookService(repository);

            var books = await service.GetAllAsync();

            Assert.IsNotNull(books);
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public async Task GivenThatNoBookExists_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(Enumerable.Empty<BookEntity>()));
            var service = new BookService(repository);

            var books = await service.GetAllAsync();

            Assert.IsNotNull(books);
            Assert.AreEqual(0, books.Count());
        }

        [TestMethod]
        public async Task GivenThatOneBookExists_WhenTryingToGetAll_ThenReturnedListContainsOneBook()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAllAsync(null, null, null))
                .IgnoreArguments()
                .Return(Task.FromResult(new List<BookEntity> { new BookEntity() { Id = 1 } }.AsEnumerable()));
            var service = new BookService(repository);

            var books = await service.GetAllAsync();

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count());
        }

        [TestMethod]
        public async Task GivenThatTenBooksExists_WhenTryingToGetAll_ThenReturnedListContainsTenBooks()
        {
            var booksToReturn = new List<BookEntity>();
            for (int i = 0; i < 10; i++)
            {
                booksToReturn.Add(new BookEntity() { Id = i });
            }
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None,null))
                .IgnoreArguments()
                .Return(Task.FromResult(booksToReturn.AsEnumerable()));
            var service = new BookService(repository);

            var books = await service.GetAllAsync();

            Assert.IsNotNull(books);
            Assert.AreEqual(10, books.Count());
        }

        [TestMethod]
        public async Task GivenThatNoBooksExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetAsync(null, CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(Enumerable.Empty<BookEntity>()));
            repository.Stub(x => x.GetAllAsQueryable()).Return(Enumerable.Empty<BookEntity>().AsQueryable());
            var service = new BookService(repository);

            var paginatedBooks = await service.GetPaginatedAsync(1, 10);

            Assert.IsNotNull(paginatedBooks);
            Assert.AreEqual(0, paginatedBooks.Count);
        }
    }
}