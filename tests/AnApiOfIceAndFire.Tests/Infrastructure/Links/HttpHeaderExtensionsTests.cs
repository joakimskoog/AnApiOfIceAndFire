using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AnApiOfIceAndFire.Infrastructure.Links;
using Xunit;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    public class HttpHeaderExtensionsTests
    {
        [Fact]
        public void LinkIsNull_WhenAddingLinkHeader_ArgumentNullExceptionIsThrown()
        {
            var response = new HttpResponseMessage();
            Link link = null;
            Assert.Throws<ArgumentNullException>(() => response.Headers.AddLinkHeader(link));
        }

        [Fact]
        public void LinkContainsTargetAndRel_WhenAddingLinkHeader_ResponseContainsCorrectLinkHeader()
        {
            var response = new HttpResponseMessage();
            var uri = new Uri("https://api.ourawesomesite.com");
            var link = new Link(uri, "next");

            response.Headers.AddLinkHeader(link);
            var linkHeader = response.Headers.GetValues("Link");

            Assert.Equal($"<{uri.OriginalString}>; rel=\"{link.Relation}\"", linkHeader.First());
        }

        [Fact]
        public void ListOfLinksIsNull_WenAddingLinkHeader_ArgumentNullExceptionIsThrown()
        {
            var response = new HttpResponseMessage();
            List<Link> links = null;
            Assert.Throws<ArgumentNullException>(() => response.Headers.AddLinkHeader(links));
        }

        [Fact]
        public void AllLinksAreCorrect_WhenAddingLinkHeader_ResponseContainsCorrectLinkHeaderValues()
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

            Assert.Equal("<https://api.ourawesomesite.com/resources/1>; rel=\"first\", <https://api.ourawesomesite.com/resources/5>; rel=\"last\", <https://api.ourawesomesite.com/resources/3>; rel=\"next\", <https://api.ourawesomesite.com/resources/1>; rel=\"prev\"", linkHeader);
        }
    }
}