using System;
using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using SimplePagedList;
using Xunit;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Links
{
    public class PagedListExtensionsTests
    {
        [Fact]
        public void PagedListIsNull_WhenCreatingPagingLinks_ArgumentNullExceptionIsThrown()
        {
            IPagedList<int> pagedList = null;
            Assert.Throws<ArgumentNullException>(() => pagedList.ToPagingLinks(new UrlHelper(new ActionContext()), "routeName"));
        }

        [Fact]
        public void UrlHelperIsNull_WhenCreatingPagingLinks_ArgumentNullExceptionIsThrown()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 1);
            Assert.Throws<ArgumentNullException>(() => pagedList.ToPagingLinks(null, "routeName"));
        }

        [Fact]
        public void RouteNameIsNull_WhenCreatingPagingLinks_ArgumentNullExceptionIsThrown()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 1);
            Assert.Throws<ArgumentNullException>(() => pagedList.ToPagingLinks(new UrlHelper(new ActionContext()), null));
        }

        [Fact]
        public void PagedListContainsNoPreviousOrNextPage_WhenCreatingPagingLinks_ListOfLinksContainsFirstAndLastLink()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 10);
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost.com");
   
            var pagingLinks = pagedList.ToPagingLinks(urlHelper.Object, "SomeRouteName").ToList();

            Assert.Equal(2, pagingLinks.Count);
            Assert.True(pagingLinks.Exists(x => x.Relation.Equals("first")));
            Assert.True(pagingLinks.Exists(x => x.Relation.Equals("last")));
        }

        [Fact]
        public void PagedListContainsNextPage_WhenCreatingPagingLinks_ListOfLinksContainsNextLink()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 1, 1);
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost.com");

            var pagingLinks = pagedList.ToPagingLinks(urlHelper.Object, "SomeRouteName").ToList();

            Assert.Equal(3, pagingLinks.Count);
            Assert.True(pagingLinks.Exists(x => x.Relation.Equals("next")));
        }

        [Fact]
        public void PagedListContainsPreviousPage_WhenCreatingPagingLinks_ListOfLinksContainsPreviousLink()
        {
            IPagedList<int> pagedList = new PagedList<int>(new List<int> { 1, 2, 3 }.AsQueryable(), 3, 1);
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost.com");

            var pagingLinks = pagedList.ToPagingLinks(urlHelper.Object, "SomeRouteName").ToList();

            Assert.Equal(3, pagingLinks.Count);
            Assert.True(pagingLinks.Exists(x => x.Relation.Equals("prev")));
        }
    }
}