using System;
using System.Collections.Generic;
using System.Web.Http;
using AnApiOfIceAndFire.Infrastructure.Links;

namespace AnApiOfIceAndFire.Results
{
    public static class HttpActionResultExtensions
    {
        public static IHttpActionResult WithLinkHeaders(this IHttpActionResult actionResult, IEnumerable<Link> links)
        {
            if (links == null) throw new ArgumentNullException(nameof(links));

            return new LinkHeaderResult(actionResult, links);
        }
    }
}