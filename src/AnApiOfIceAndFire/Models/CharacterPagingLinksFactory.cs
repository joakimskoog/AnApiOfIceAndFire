using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Controllers.v1;
using AnApiOfIceAndFire.Data.Characters;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;
using SimplePagedList;

namespace AnApiOfIceAndFire.Models
{
    public class CharacterPagingLinksFactory : IPagingLinksFactory<CharacterFilter>
    {
        public IEnumerable<Link> Create<T>(IPagedList<T> pagedList, IUrlHelper urlHelper, CharacterFilter filter)
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

            return pagedList.ToPagingLinks(urlHelper, CharactersController.MultipleCharactersRouteName, routeValues);
        }
    }
    
}