using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AnApiOfIceAndFire
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Use indented to make it more readable for the consumer, using gzip is better for bandwidth anyway.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            //Use camelCase for naming of properties since it's more of a standard
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Use the ISO format instead of Microsoft format. This is to make it easier for the consumer to parse the date, especially if they don't use a Microsoft stack themselves.
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            //We want to represent our enums with their names instead of their numerical values. This is to make it more readable for the consumer.
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            //Remove the possibility to serialize models to XML since we don't want to support that at the moment.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
