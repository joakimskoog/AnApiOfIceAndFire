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

            return pagedList.ToPagingLinks(urlHelper, routeName, new Dictionary<string, object>());
        }

        public static IEnumerable<Link> ToPagingLinks<T>(this IPagedList<T> pagedList, UrlHelper urlHelper, string routeName, IDictionary<string, object> routeValues)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));
            if (routeName == null) throw new ArgumentNullException(nameof(routeName));

            var links = new List<Link>();

            if (pagedList.NextPageNumber.HasValue)
            {
                var nextPageLinkRouteValues = new Dictionary<string, object>(routeValues)
                {
                    {"page", pagedList.NextPageNumber.Value},
                    {"pageSize", pagedList.PageSize}
                };
                var target = urlHelper.Link(routeName, nextPageLinkRouteValues);
                links.Add(new Link(new Uri(target), "next"));
            }
            if (pagedList.PreviousPageNumber.HasValue)
            {
                var previousPageLinkRouteValues = new Dictionary<string, object>(routeValues)
                {
                    {"page", pagedList.PreviousPageNumber.Value},
                    {"pageSize", pagedList.PageSize}
                };
                var target = urlHelper.Link(routeName, previousPageLinkRouteValues);
                links.Add(new Link(new Uri(target), "prev"));
            }
            var firstPageLinkRouteValues = new Dictionary<string, object>(routeValues)
            {
                 {"page", pagedList.FirstPageNumber},
                 {"pageSize", pagedList.PageSize}
            };
            var firstTarget = urlHelper.Link(routeName, firstPageLinkRouteValues);
            links.Add(new Link(new Uri(firstTarget), "first"));

            var lastPageLinkRouteValues = new Dictionary<string, object>(routeValues)
            {
                 {"page", pagedList.LastPageNumber},
                 {"pageSize", pagedList.PageSize}
            };
            var lastTarget = urlHelper.Link(routeName, lastPageLinkRouteValues);
            links.Add(new Link(new Uri(lastTarget), "last"));

            return links;
        }
    }
}