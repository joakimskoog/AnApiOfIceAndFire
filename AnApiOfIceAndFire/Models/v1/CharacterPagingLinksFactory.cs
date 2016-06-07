using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models.Filters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Models.v1
{
    public class CharacterPagingLinksFactory : IPagingLinksFactory<CharacterFilter>
    {
        public IEnumerable<Link> Create<T>(IPagedList<T> pagedList, UrlHelper urlHelper, CharacterFilter filter)
        {
            if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var routeValues = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                routeValues.Add("name", filter.Name);
            }
            if (!string.IsNullOrEmpty(filter.Culture))
            {
                routeValues.Add("culture", filter.Culture);
            }
            if (!string.IsNullOrEmpty(filter.Born))
            {
                routeValues.Add("born", filter.Born);
            }
            if (!string.IsNullOrEmpty(filter.Died))
            {
                routeValues.Add("died", filter.Died);
            }
            if (filter.IsAlive.HasValue)
            {
                routeValues.Add("isAlive", filter.IsAlive.Value);
            }
            if (filter.Gender.HasValue)
            {
                routeValues.Add("gender", filter.Gender.Value);
            }

            return pagedList.ToPagingLinks(urlHelper, CharacterLinkCreator.MultipleCharactersRouteName, routeValues);
        }
    }
}