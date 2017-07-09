using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimplePagedList;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public interface IPagingLinksFactory<in TFilter>
    {
        IEnumerable<Link> Create<T>(IPagedList<T> pagedList, IUrlHelper urlHelper, TFilter filter);
    }
}