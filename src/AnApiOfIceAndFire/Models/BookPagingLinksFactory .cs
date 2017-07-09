using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;
using SimplePagedList;

namespace AnApiOfIceAndFire.Models
{
    public class BookPagingLinksFactory : IPagingLinksFactory<BookFilter>
    {
        public IEnumerable<Link> Create<T>(IPagedList<T> pagedList, IUrlHelper urlHelper, BookFilter filter)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var routeValues = new Dictionary<string, object>();
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

            return pagedList.ToPagingLinks(urlHelper, BooksController.MultipleBooksRouteName, routeValues);
        }
    }
}