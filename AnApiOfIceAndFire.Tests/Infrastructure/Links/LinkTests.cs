using System;
using System.Diagnostics.CodeAnalysis;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LinkTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatTargetIsNull_WhenConstructingLink_ThenArgumentNullExceptionIsThrown()
        {
            var link = new Link(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatRelationIsNull_WhenConstructingLink_ThenArgumentNullExceptionIsThrown()
        {
            var link = new Link(new Uri("https://localhost.com"), null);
        }
    }
}