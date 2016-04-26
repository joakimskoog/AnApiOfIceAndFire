using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Models.v1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.Controllers
{
    [TestClass]
    public class EndpointsControllerTests
    {
        [TestMethod]
        public void GivenThatABunchOfControllersExists_WhenTryingToGet_ThenReturnedEndpointsContainsCorrectUrls()
        {
            var urlHelper = CreateUrlHelper("http://localhost.com");
            var controller = new EndpointsController()
            {
                Url = urlHelper
            };

            var endpoints = controller.Get() as OkNegotiatedContentResult<Endpoints>;
            var endpointsContent = endpoints.Content;

            Assert.IsNotNull(endpoints);
            Assert.AreEqual("http://localhost.com/api/books", endpointsContent.Books);
            Assert.AreEqual("http://localhost.com/api/characters", endpointsContent.Characters);
            Assert.AreEqual("http://localhost.com/api/houses", endpointsContent.Houses);
        }

        private static UrlHelper CreateUrlHelper(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: "BooksApi",
                routeTemplate: "api/books/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            configuration.Routes.MapHttpRoute(
               name: "CharactersApi",
               routeTemplate: "api/characters/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            configuration.Routes.MapHttpRoute(
               name: "HousesApi",
               routeTemplate: "api/houses/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            requestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);

            var urlHelper = new UrlHelper(requestMessage);

            return urlHelper;
        }
    }
}