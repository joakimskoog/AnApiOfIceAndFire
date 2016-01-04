using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using SimplePagination;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PagedListExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatPagedListIsNull_WhenCreatingPagingLinks_ThenArgumentNullExceptionIsThrown()
        {
            IPagedList<int> pagedList = null;
            pagedList.ToPagingLinks(new UrlHelper(), "routeName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatUrlHelperIsNull_WhenCreatingPagingLinks_ThenArgumentNullExceptionIsThrown()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 1);
            pagedList.ToPagingLinks(null, "routeName");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenThatRouteNameIsNull_WhenCreatingPagingLinks_ThenArgumentNullExceptionIsThrown()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 1);
            pagedList.ToPagingLinks(new UrlHelper(), null);
        }

        [TestMethod]
        public void GivenThatPagedListContainsNoPreviousOrNextPage_WhenCreatingPagingLinks_ThenListOfLinksContainsFirstAndLastLink()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 10);
            var urlHelper = MockRepository.GenerateMock<UrlHelper>();
            urlHelper.Stub(x => x.Link(Arg<string>.Is.Anything, Arg<object>.Is.Anything)).Return("https://localhost.com");
            var pagingLinks = pagedList.ToPagingLinks(urlHelper, "SomeRouteName").ToList();

            Assert.AreEqual(2, pagingLinks.Count);
            Assert.IsTrue(pagingLinks.Exists(x => x.Relation.Equals("first")));
            Assert.IsTrue(pagingLinks.Exists(x => x.Relation.Equals("last")));
        }

        [TestMethod]
        public void GivenThatPagedListContainsNextPage_WhenCreatingPagingLinks_ThenListOfLinksContainsNextLink()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 1);
            var urlHelper = MockRepository.GenerateMock<UrlHelper>();
            urlHelper.Stub(x => x.Link(Arg<string>.Is.Anything, Arg<object>.Is.Anything)).Return("https://localhost.com");
            var pagingLinks = pagedList.ToPagingLinks(urlHelper, "SomeRouteName").ToList();

            Assert.AreEqual(3, pagingLinks.Count);
            Assert.IsTrue(pagingLinks.Exists(x => x.Relation.Equals("next")));
        }

        [TestMethod]
        public void GivenThatPagedListContainsPreviousPage_WhenCreatingPagingLinks_ThenListOfLinksContainsPreviousLink()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 3, 1);
            var urlHelper = MockRepository.GenerateMock<UrlHelper>();
            urlHelper.Stub(x => x.Link(Arg<string>.Is.Anything, Arg<object>.Is.Anything)).Return("https://localhost.com");
            var pagingLinks = pagedList.ToPagingLinks(urlHelper, "SomeRouteName").ToList();

            Assert.AreEqual(3, pagingLinks.Count);
            Assert.IsTrue(pagingLinks.Exists(x => x.Relation.Equals("prev")));
        }
    }
}