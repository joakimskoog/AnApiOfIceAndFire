using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Houses;
using Geymsla;
using Geymsla.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace AnApiOfIceAndFire.Domain.Tests.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HouseServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatRepositoryIsNull_WhenCreatingService_ThenArgumentNullExceptionIsThrown()
        {
            var service = new HouseService(null);
        }

        [TestMethod]
        public async Task GivenThatNoCharacterExists_WhenTryingToGetById_ThenReturnedCharacterIsNull()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetAsync(null,CancellationToken.None,null)).IgnoreArguments().Return(Task.FromResult(Enumerable.Empty<HouseEntity>()));
            var service = new HouseService(repository);

            var character = await service.GetAsync(1);

            Assert.IsNull(character);
        }

        [TestMethod]
        public async Task GivenThatCharacterWithGivenIdExists_WhenTryingToGetById_ThenReturnedCharacterHasSameId()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetAsync(null, CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(new List<HouseEntity>
                {
                    new HouseEntity() {Id = 1}
                }.AsEnumerable()));
            var service = new HouseService(repository);

            var character = await service.GetAsync(1);

            Assert.IsNotNull(character);
            Assert.AreEqual(1, character.Identifier);
        }

        [TestMethod]
        public async Task GivenThatNoCharactersExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetPaginatedListAsync(null, 0, 0, CancellationToken.None))
                .IgnoreArguments()
                .Return(Task.FromResult((IPagedList<HouseEntity>)new PagedList<HouseEntity>(Enumerable.Empty<HouseEntity>().AsQueryable(), 1, 10)));
            var service = new HouseService(repository);

            var paginatedCharacters = await service.GetPaginatedAsync(1, 10);

            Assert.IsNotNull(paginatedCharacters);
            Assert.AreEqual(0, paginatedCharacters.Count);
        }
    }
}