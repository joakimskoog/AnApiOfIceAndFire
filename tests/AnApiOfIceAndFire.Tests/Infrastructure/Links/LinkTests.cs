using System;
using AnApiOfIceAndFire.Infrastructure.Links;
using Xunit;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    public class LinkTests
    {
        [Fact]
        public void GivenThatLinkHasNoRelation_WhenConstructingLinkHeader_ThenLinkHeaderOnlyContainsCorrectUrl()
        {
            var uri = new Uri("https://api.ourawesomesite.com");
            var link = new Link(uri);

            var linkHeader = link.ToLinkHeader();

            Assert.Equal($"<{uri.OriginalString}>", linkHeader);
        }

        [Fact]
        public void GivenThatLinkContainsParameters_WhenConstructingLinkHeader_ThenUrlParametersAreCorrectInLinkHeader()
        {
            var uri = new Uri("https://api.ourawesomesite.com/resources?parameterOne=parameterValueOne");
            var link = new Link(uri);

            var linkHeader = link.ToLinkHeader();

            Assert.Equal($"<{uri.OriginalString}>", linkHeader);
        }

        [Fact]
        public void GivenThatLinkContainsRelation_WhenConstructingLinkHeader_ThenUrlAndRelationIsCorrectInLinkHeader()
        {
            var uri = new Uri("https://api.ourawesomesite.com/resources?parameterOne=parameterValueOne");
            var link = new Link(uri, "next");

            var linkHeader = link.ToLinkHeader();

            Assert.Equal($"<{uri.OriginalString}>; rel=\"{link.Relation}\"", linkHeader);
        }
    }
}