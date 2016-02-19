using System.Collections.Generic;
using System.Web.Http.Routing;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public interface IPagingLinksFactory<in TFilter>
    {
        IEnumerable<Link> Create<T>(IPagedList<T> pagedList, UrlHelper urlHelper, TFilter filter);
    }
}