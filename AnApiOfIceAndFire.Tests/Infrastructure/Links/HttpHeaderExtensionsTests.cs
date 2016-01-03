using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HttpHeaderExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatLinkIsNull_WhenAddingLinkheader_ThenArgumentNullExceptionIsThrown()
        {
            var response = new HttpResponseMessage();
            Link link = null;
            response.Headers.AddLinkHeader(link);
        }

        [TestMethod]
        public void GivenThatLinkContainsTargetAndRel_WhenAddingLinkHeader_ThenResponseContainsCorrectLinkHeader()
        {
            var response = new HttpResponseMessage();
            var uri = new Uri("https://api.ourawesomesite.com");
            var link = new Link(uri, "next");

            response.Headers.AddLinkHeader(link);
            var linkHeader = response.Headers.GetValues("Link");

            Assert.AreEqual($"<{uri.OriginalString}>; rel=\"{link.Relation}\"", linkHeader.First());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatListOfLinksIsNull_WenAddingLinkHeader_ThenArgumentNullExceptionIsThrown()
        {
            var response = new HttpResponseMessage();
            List<Link> links = null;
            response.Headers.AddLinkHeader(links);
        }

        [TestMethod]
        public void GivenThatAllLinksAreCprrect_WhenAddingLinkHeader_ThenResponseContainsCorrectLinkHeaderValues()
        {
            var response = new HttpResponseMessage();
            var links = new List<Link>()
            {
                new Link(new Uri("https://api.ourawesomesite.com/resources/1"), "first"),
                new Link(new Uri("https://api.ourawesomesite.com/resources/5"), "last"),
                new Link(new Uri("https://api.ourawesomesite.com/resources/3"), "next"),
                new Link(new Uri("https://api.ourawesomesite.com/resources/1"), "prev")
            };
            
            response.Headers.AddLinkHeader(links);
            var linkHeader = response.Headers.GetValues("Link").First();

            Assert.AreEqual("<https://api.ourawesomesite.com/resources/1>; rel=\"first\", <https://api.ourawesomesite.com/resources/5>; rel=\"last\", <https://api.ourawesomesite.com/resources/3>; rel=\"next\", <https://api.ourawesomesite.com/resources/1>; rel=\"prev\"", linkHeader);
        }
    }
}