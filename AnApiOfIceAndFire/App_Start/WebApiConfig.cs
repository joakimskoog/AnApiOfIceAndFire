using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using AnApiOfIceAndFire.Infrastructure;
using CacheCow.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using static AnApiOfIceAndFire.Models.v0.BookLinkCreator;
using static AnApiOfIceAndFire.Models.v0.CharacterLinkCreator;
using static AnApiOfIceAndFire.Models.v0.HouseLinkCreator;

namespace AnApiOfIceAndFire
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Adds a message handler that takes care of all caching for us (ETag and Last-Modified)
            config.MessageHandlers.Add(new CachingHandler(config));

            //Use indented to make it more readable for the consumer, using gzip is better for bandwidth anyway.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            //Use camelCase for naming of properties since it's more of a standard
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Use the ISO format instead of Microsoft format. This is to make it easier for the consumer to parse the date, especially if they don't use a Microsoft stack themselves.
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            //We want to represent our enums with their names instead of their numerical values. This is to make it more readable for the consumer.
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            //Add our own media type to enable versioning via the accept header. Make this sexier, maybe use reflection to reflect all current namespaces?
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(AcceptHeaderControllerSelector.AllowedAcceptHeaderMediaType)
            {
                Parameters = { new NameValueHeaderValue(AcceptHeaderControllerSelector.AllowedAcceptHeaderMediaTypeParamter, "0") }
            });

            //Remove the possibility to serialize models to XML since we don't want to support that at the moment.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Replace the default IHttpControllerSelector with our own that selects controllers based on Accept header and namespaces.
            config.Services.Replace(typeof(IHttpControllerSelector), new AcceptHeaderControllerSelector(config));

            //This is not super sexy but it's needed to be able to create URLs to other resources.
            //We can't use RouteAttributes since that messes with our controller selector, thus this is the "best" solution we can use.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: BookRouteName,
                routeTemplate: "api/books/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: CharacterRouteName,
                routeTemplate: "api/characters/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: HouseRouteName,
                routeTemplate: "api/houses/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
