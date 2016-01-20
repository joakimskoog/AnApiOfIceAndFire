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
    public class CharacterServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatRepositoryIsNull_WhenCreatingService_ThenArgumentNullExceptionIsThrown()
        {
            var service = new CharacterService(null);
        }

        [TestMethod]
        public async Task GivenThatNoCharacterExists_WhenTryingToGetById_ThenReturnedcharacterIsNull()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAsync(null,CancellationToken.None, null)).IgnoreArguments().Return(Task.FromResult(Enumerable.Empty<CharacterEntity>()));
            var service = new CharacterService(repository);

            var character = await service.GetAsync(1);

            Assert.IsNull(character);
        }

        [TestMethod]
        public async Task GivenThatCharacterWithGivenIdExists_WhenTryingToGetById_ThenReturnedcharacterHasSameId()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAsync(null, CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(new List<CharacterEntity>
                {
                    new CharacterEntity() {Id = 1}
                }.AsEnumerable()));
            var service = new CharacterService(repository);

            var character = await service.GetAsync(1);

            Assert.IsNotNull(character);
            Assert.AreEqual(1, character.Identifier);
        }

        [TestMethod]
        public async Task GivenThatRepositoryReturnsNull_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAllAsync(null, null, null))
                .IgnoreArguments()
                .Return(Task.FromResult((IEnumerable<CharacterEntity>)null));
            var service = new CharacterService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(0, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatNoCharacterExists_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(Enumerable.Empty<CharacterEntity>()));
            var service = new CharacterService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(0, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatOneCharacterExists_WhenTryingToGetAll_ThenReturnedListContainsOnecharacter()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(new List<CharacterEntity> { new CharacterEntity() { Id = 1 } }.AsEnumerable()));
            var service = new CharacterService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(1, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatTenCharactersExists_WhenTryingToGetAll_ThenReturnedListContainsTencharacters()
        {
            var charactersToReturn = new List<CharacterEntity>();
            for (int i = 0; i < 10; i++)
            {
                charactersToReturn.Add(new CharacterEntity() { Id = i });
            }
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAllAsync(CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(charactersToReturn.AsEnumerable()));
            var service = new CharacterService(repository);

            var characters = await service.GetAllAsync();

            Assert.IsNotNull(characters);
            Assert.AreEqual(10, characters.Count());
        }

        [TestMethod]
        public async Task GivenThatNoCharactersExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IReadOnlyRepository<CharacterEntity, int>>();
            repository.Stub(x => x.GetAsync(null, CancellationToken.None, null))
                .IgnoreArguments()
                .Return(Task.FromResult(Enumerable.Empty<CharacterEntity>()));
            repository.Stub(x => x.GetAllAsQueryable()).Return(Enumerable.Empty<CharacterEntity>().AsQueryable());
            var service = new CharacterService(repository);

            var paginatedCharacters = await service.GetPaginatedAsync(1, 10);

            Assert.IsNotNull(paginatedCharacters);
            Assert.AreEqual(0, paginatedCharacters.Count);
        }
    }
}