using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public static class PagedListExtensions
    {
        public static IEnumerable<Link> ToPagingLinks<T>(this IPagedList<T> pagedList, UrlHelper urlHelper, string routeName)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));
            if (routeName == null) throw new ArgumentNullException(nameof(routeName));

            var links = new List<Link>();

            if (pagedList.NextPageNumber.HasValue)
            {
                var target = urlHelper.Link(routeName, new { page = pagedList.NextPageNumber.Value, pageSize = pagedList.PageSize });
                links.Add(new Link(new Uri(target), "next"));
            }
            if (pagedList.PreviousPageNumber.HasValue)
            {
                var target = urlHelper.Link(routeName, new { page = pagedList.PreviousPageNumber.Value, pageSize = pagedList.PageSize });
                links.Add(new Link(new Uri(target), "prev"));
            }
            var firstTarget = urlHelper.Link(routeName, new { page = pagedList.FirstPageNumber, pageSize = pagedList.PageSize });
            links.Add(new Link(new Uri(firstTarget), "first"));

            var lastTarget = urlHelper.Link(routeName, new { page = pagedList.LastPageNumber, pageSize = pagedList.PageSize });
            links.Add(new Link(new Uri(lastTarget), "last"));

            return links;
        }
    }
}