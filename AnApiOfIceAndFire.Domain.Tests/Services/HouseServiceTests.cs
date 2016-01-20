using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        //[TestMethod]
        //public async Task GivenThatRepositoryReturnsNull_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        //{
        //    var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
        //    repository.Stub(x => x.GetAllAsync(CancellationToken.None,null))
        //        .IgnoreArguments()
        //        .Return(Task.FromResult(Enumerabl));
        //    var service = new HouseService(repository);

        //    var characters = await service.GetAllAsync();

        //    Assert.IsNotNull(characters);
        //    Assert.AreEqual(0, characters.Count());
        //}

        [TestMethod]
        public async Task GivenThatNoCharacterExists_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetAllAsync(null, null, null))
                .IgnoreArguments()
                .Return(Task.FromResult(Enumerable.Empty<HouseEntity>()));
            var service = new HouseService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(0, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatOneCharacterExists_WhenTryingToGetAll_ThenReturnedListContainsOneCharacter()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None,null))
                .IgnoreArguments()
                .Return(Task.FromResult(new List<HouseEntity> { new HouseEntity() { Id = 1 } }.AsEnumerable()));
            var service = new HouseService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(1, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatTenCharactersExists_WhenTryingToGetAll_ThenReturnedListContainsTenCharacters()
        {
            var charactersToReturn = new List<HouseEntity>();
            for (int i = 0; i < 10; i++)
            {
                charactersToReturn.Add(new HouseEntity() { Id = i });
            }
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None,null))
                .IgnoreArguments()
                .Return(Task.FromResult(charactersToReturn.AsEnumerable()));
            var service = new HouseService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(10, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatNoCharactersExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<HouseEntity,int>>();
            repository.Stub(x => x.GetAsync(null, CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(Enumerable.Empty<HouseEntity>()));
            repository.Stub(x => x.GetAllAsQueryable()).Return(Enumerable.Empty<HouseEntity>().AsQueryable());
            var service = new HouseService(repository);

            var paginatedCharacters = await service.GetPaginatedAsync(1, 10);

            Assert.IsNotNull(paginatedCharacters);
            Assert.AreEqual(0, paginatedCharacters.Count);
        }
    }
}