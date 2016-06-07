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
            var urlHelper = Helper.CreateUrlHelper("http://localhost.com");
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
    }
}