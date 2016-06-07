using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Models.v1;

namespace AnApiOfIceAndFire.Tests
{
    public static class Helper
    {
        public static UrlHelper CreateUrlHelper(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: BookLinkCreator.SingleBookRouteName,
                routeTemplate: "api/books/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            configuration.Routes.MapHttpRoute(
                name: BookLinkCreator.MultipleBooksRouteName,
                routeTemplate: "api/books"
            );
            configuration.Routes.MapHttpRoute(
               name: CharacterLinkCreator.SingleCharacterRouteName,
               routeTemplate: "api/characters/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            configuration.Routes.MapHttpRoute(
               name: CharacterLinkCreator.MultipleCharactersRouteName,
               routeTemplate: "api/characters"
           );
            configuration.Routes.MapHttpRoute(
               name: HouseLinkCreator.SingleHouseRouteName,
               routeTemplate: "api/houses/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            configuration.Routes.MapHttpRoute(
               name: HouseLinkCreator.MultipleHousesRouteName,
               routeTemplate: "api/houses"
           );
            requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);

            var urlHelper = new UrlHelper(requestMessage);

            return urlHelper;
        }
    }
}