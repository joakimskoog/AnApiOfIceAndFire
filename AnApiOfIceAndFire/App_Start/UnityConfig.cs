using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;
using EntityFrameworkRepository;
using Microsoft.Practices.Unity;
using MediaType = AnApiOfIceAndFire.Domain.Models.MediaType;

namespace AnApiOfIceAndFire
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();    

            container.RegisterType<IEntityDbContext, AnApiOfIceAndFireContext>();
            container.RegisterType<IRepositoryWithIntKey<BookEntity>, EFRepositoryWithIntKey<BookEntity>>();
            container.RegisterType<IRepositoryWithIntKey<CharacterEntity>, TestCharacterRepo>();
            container.RegisterType<IRepositoryWithIntKey<HouseEntity>, EFRepositoryWithIntKey<HouseEntity>>();
            container.RegisterType<IModelMapper<MediaType, Models.v0.MediaType>, MediaTypeMapper>();
            container.RegisterType<IModelMapper<IBook, Book>, BookMapper>();
            container.RegisterType<IModelMapper<ICharacter, Character>, CharacterMapper>();
            container.RegisterType<IModelMapper<IHouse, House>, HouseMapper>();
            container.RegisterType<IModelService<IBook>, BookService>();
            container.RegisterType<IModelService<ICharacter>, CharacterService>();
            container.RegisterType<IModelService<IHouse>, HouseService>();
        }
    }

    public class TestCharacterRepo : IRepositoryWithIntKey<CharacterEntity>
    {
        public IQueryable<CharacterEntity> GetAll(Expression<Func<CharacterEntity, bool>> filter = null, Func<IQueryable<CharacterEntity>, IOrderedQueryable<CharacterEntity>> orderBy = null, params Expression<Func<CharacterEntity, object>>[] includeProperties)
        {
            return new List<CharacterEntity>
            {
                new CharacterEntity() {Identifier = 1},
                new CharacterEntity() {Identifier = 2},
                new CharacterEntity() {Identifier = 3},
                new CharacterEntity() {Identifier = 4},
                new CharacterEntity() {Identifier = 5}
            }.AsQueryable();
        }

        public CharacterEntity GetById(int id, params Expression<Func<CharacterEntity, object>>[] includePropertie)
        {
            return new CharacterEntity()
            {
                Identifier = id
            };
        }

        public void Add(CharacterEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CharacterEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CharacterEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }
    }
}