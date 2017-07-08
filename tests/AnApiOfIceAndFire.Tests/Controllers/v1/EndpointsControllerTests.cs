using System;
using System.Dynamic;
using System.Net.Http;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;

namespace AnApiOfIceAndFire.Tests.Controllers.v1
{
    public class EndpointsControllerTests
    {
        private const string BooksUrl = "http://localhost:55686/api/books";
        private const string CharactersUrl = "http://localhost:55686/api/characters";
        private const string HousesUrl = "http://localhost:55686/api/houses";

        [Fact]
        public void CorrectRouteNames_WhenLinkingToOtherResources_GeneratedUrlsAreCorrect()
        {
            var url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.Is<string>(s => string.Equals(BooksController.MultipleBooksRouteName, s)), It.IsAny<object>())).Returns(() => BooksUrl);
            url.Setup(x => x.Link(It.Is<string>(s => string.Equals(CharactersController.MultipleCharactersRouteName, s)), It.IsAny<object>())).Returns(() => CharactersUrl);
            url.Setup(x => x.Link(It.Is<string>(s => string.Equals(HousesController.MultipleHousesRouteName, s)), It.IsAny<object>())).Returns(() => HousesUrl);
            var sut = new EndpointsController
            {
                Url = url.Object
            };

            var endpoints = (sut.Get() as OkObjectResult)?.Value as Endpoints;

            Assert.NotNull(endpoints);
            Assert.Equal(BooksUrl, endpoints.Books);
            Assert.Equal(CharactersUrl, endpoints.Characters);
            Assert.Equal(HousesUrl, endpoints.Houses);
        }
    }
}