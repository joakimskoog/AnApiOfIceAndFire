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
    public class HouseServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatRepositoryIsNull_WhenCreatingService_ThenArgumentNullExceptionIsThrown()
        {
            var service = new HouseService(null);
        }

        [TestMethod]
        public void GivenThatNoCharacterExists_WhenTryingToGetById_ThenReturnedCharacterIsNull()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetById(0)).IgnoreArguments().Return(null);
            var service = new HouseService(repository);

            var character = service.Get(1);

            Assert.IsNull(character);
        }

        [TestMethod]
        public void GivenThatCharacterWithGivenIdExists_WhenTryingToGetById_ThenReturnedCharacterHasSameId()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.Get(0, null)).IgnoreArguments().Return(new HouseEntity()
            {
                Id = 1
            });
            var service = new HouseService(repository);

            var character = service.Get(1);

            Assert.IsNotNull(character);
            Assert.AreEqual(1, character.Identifier);
        }

        [TestMethod]
        public void GivenThatRepositoryReturnsNull_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(null);
            var service = new HouseService(repository);

            var characters = service.GetAll();

            Assert.IsNotNull(characters);
            Assert.AreEqual(0, characters.Count());
        }

        [TestMethod]
        public void GivenThatNoCharacterExists_WhenTryingToGetAll_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(Enumerable.Empty<HouseEntity>().AsQueryable());
            var service = new HouseService(repository);

            var characters = service.GetAll();

            Assert.IsNotNull(characters);
            Assert.AreEqual(0, characters.Count());
        }

        [TestMethod]
        public void GivenThatOneCharacterExists_WhenTryingToGetAll_ThenReturnedListContainsOneCharacter()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(new List<HouseEntity> { new HouseEntity() { Id = 1 } }.AsQueryable());
            var service = new HouseService(repository);

            var characters = service.GetAll();

            Assert.IsNotNull(characters);
            Assert.AreEqual(1, characters.Count());
        }

        [TestMethod]
        public void GivenThatTenCharactersExists_WhenTryingToGetAll_ThenReturnedListContainsTenCharacters()
        {
            var charactersToReturn = new List<HouseEntity>();
            for (int i = 0; i < 10; i++)
            {
                charactersToReturn.Add(new HouseEntity() { Id = i });
            }
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetAll())
                .IgnoreArguments()
                .Return(charactersToReturn.AsQueryable());
            var service = new HouseService(repository);

            var characters = service.GetAll();

            Assert.IsNotNull(characters);
            Assert.AreEqual(10, characters.Count());
        }

        [TestMethod]
        public void GivenThatRepositoryReturnsNull_WhenTryingToGetPaginated_ThenReturnedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(null);
            var service = new HouseService(repository);

            var characters = service.GetPaginated(1, 10);

            Assert.IsNotNull(characters);
            Assert.AreEqual(0, characters.Count());
        }

        [TestMethod]
        public void GivenThatNoCharactersExists_WhenTryingToGetPaginated_ThenPaginatedListIsEmpty()
        {
            var repository = MockRepository.GenerateMock<IRepositoryWithIntKey<HouseEntity>>();
            repository.Stub(x => x.GetAll(null, null, null))
                .IgnoreArguments()
                .Return(Enumerable.Empty<HouseEntity>().AsQueryable());
            var service = new HouseService(repository);

            var paginatedCharacters = service.GetPaginated(1, 10);

            Assert.IsNotNull(paginatedCharacters);
            Assert.AreEqual(0, paginatedCharacters.Count);
        }
    }
}