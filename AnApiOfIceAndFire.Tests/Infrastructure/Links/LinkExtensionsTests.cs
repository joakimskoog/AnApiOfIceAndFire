using System;
using System.Diagnostics.CodeAnalysis;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LinkExtensionsTests
    {
        [TestMethod]
        public void GivenThatLinkHasNoRelation_WhenConstructingLinkHeader_ThenLinkHeaderOnlyContainsCorrectUrl()
        {
            var uri = new Uri("https://api.ourawesomesite.com");
            var link = new Link(uri);

            var linkHeader = link.ToLinkHeader();

            Assert.AreEqual($"<{uri.OriginalString}>", linkHeader);
        }

        [TestMethod]
        public void GivenThatLinkContainsParameters_WhenConstructingLinkHeader_ThenUrlParametersAreCorrectInLinkHeader()
        {
            var uri = new Uri("https://api.ourawesomesite.com/resources?parameterOne=parameterValueOne");
            var link = new Link(uri);

            var linkHeader = link.ToLinkHeader();

            Assert.AreEqual($"<{uri.OriginalString}>", linkHeader);
        }

        [TestMethod]
        public void GivenThatLinkContainsRelation_WhenConstructingLinkHeader_ThenUrlAndRelationIsCorrectInLinkHeader()
        {
            var uri = new Uri("https://api.ourawesomesite.com/resources?parameterOne=parameterValueOne");
            var link = new Link(uri, "next");

            var linkHeader = link.ToLinkHeader();

            Assert.AreEqual($"<{uri.OriginalString}>; rel=\"{link.Relation}\"", linkHeader);
        }
    }
}