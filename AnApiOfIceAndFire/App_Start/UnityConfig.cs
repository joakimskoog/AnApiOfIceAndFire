using System;
using System.Data.Entity;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Domain.Services;
using AnApiOfIceAndFire.Infrastructure;
using AnApiOfIceAndFire.Models.v0;
using AnApiOfIceAndFire.Models.v0.Mappers;
using Geymsla;
using Geymsla.EntityFramework;
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

            container.RegisterType<ISecondLevelCacheSettings, DefaultSecondLevelCacheSettings>(new InjectionConstructor(true, TimeSpan.FromHours(12)));
            container.RegisterType<DbContext, AnApiOfIceAndFireContext>();
            container.RegisterType<IReadOnlyRepository<BookEntity, int>, EntityFrameworkRepository<BookEntity, int>>();
            container.RegisterType<IReadOnlyRepository<CharacterEntity, int>, EntityFrameworkRepository<CharacterEntity, int>>();
            container.RegisterType<IReadOnlyRepository<HouseEntity, int>, EntityFrameworkRepository<HouseEntity, int>>();
            container.RegisterType<IModelMapper<MediaType, Models.v0.MediaType>, MediaTypeMapper>();
            container.RegisterType<IModelMapper<IBook, Book>, BookMapper>();
            container.RegisterType<IModelMapper<ICharacter, Character>, CharacterMapper>();
            container.RegisterType<IModelMapper<IHouse, House>, HouseMapper>();
            container.RegisterType<IModelService<IBook,BookFilter>, BookService>();
            //container.RegisterType<IModelService<ICharacter>, CharacterService>();
            //container.RegisterType<IModelService<IHouse>, HouseService>();
            container.RegisterType<IApiSettings, WebConfigSettings>();
        }
    }
}