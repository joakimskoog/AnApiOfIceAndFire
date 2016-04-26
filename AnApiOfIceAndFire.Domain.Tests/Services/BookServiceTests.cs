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
using Geymsla.Collections;
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
        public async Task GivenThatNoBooksExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<BookEntity,int>>();
            repository.Stub(x => x.GetPaginatedListAsync(null, 0, 0, CancellationToken.None))
                .IgnoreArguments()
                .Return(Task.FromResult((IPagedList<BookEntity>)new PagedList<BookEntity>(Enumerable.Empty<BookEntity>().AsQueryable(), 1, 10)));
            var service = new BookService(repository);

            var paginatedBooks = await service.GetPaginatedAsync(1, 10);

            Assert.IsNotNull(paginatedBooks);
            Assert.AreEqual(0, paginatedBooks.Count);
        }
    }
}