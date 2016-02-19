using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Models.v1
{
    public class BookPagingLinksFactory : IPagingLinksFactory<BookFilter>
    {
        public IEnumerable<Link> Create<T>(IPagedList<T> pagedList, UrlHelper urlHelper, BookFilter filter)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));

            var routeValues = new Dictionary<string,object>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                routeValues.Add("name", filter.Name);
            }
            if (filter.FromReleaseDate.HasValue)
            {
                routeValues.Add("fromReleaseDate", filter.FromReleaseDate);
            }
            if (filter.ToReleaseDate.HasValue)
            {
                routeValues.Add("toReleaseDate", filter.ToReleaseDate.Value);
            }

            return pagedList.ToPagingLinks(urlHelper, BookLinkCreator.BookRouteName, routeValues);
        }
    }
}