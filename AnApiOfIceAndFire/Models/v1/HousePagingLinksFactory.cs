using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Models.v1
{
    public class HousePagingLinksFactory : IPagingLinksFactory<HouseFilter>
    {
        public IEnumerable<Link> Create<T>(IPagedList<T> pagedList, UrlHelper urlHelper, HouseFilter filter)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));

            var routeValues = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                routeValues.Add("name", filter.Name);
            }
            if (!string.IsNullOrEmpty(filter.Region))
            {
                routeValues.Add("region", filter.Region);
            }
            if (!string.IsNullOrEmpty(filter.Words))
            {
                routeValues.Add("words", filter.Words);
            }
            if (filter.HasWords.HasValue)
            {
                routeValues.Add("hasWords", filter.HasWords.Value);
            }
            if (filter.HasTitles.HasValue)
            {
                routeValues.Add("hasTitles", filter.HasTitles.Value);
            }
            if (filter.HasSeats.HasValue)
            {
                routeValues.Add("hasSeats", filter.HasSeats.Value);
            }
            if (filter.HasDiedOut.HasValue)
            {
                routeValues.Add("hasDiedOut", filter.HasDiedOut.Value);
            }
            if (filter.HasAncestralWeapons.HasValue)
            {
                routeValues.Add("hasAncestralWeapons", filter.HasAncestralWeapons.Value);
            }
    
            return pagedList.ToPagingLinks(urlHelper, HouseLinkCreator.HouseRouteName, routeValues);
        }
    }
}