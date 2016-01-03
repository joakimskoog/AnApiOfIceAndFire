using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using AnApiOfIceAndFire.Infrastructure.Links;

namespace AnApiOfIceAndFire.Results
{
    public class LinkHeaderResult : ChainedResult
    {
        private readonly IEnumerable<Link> _links;

        public LinkHeaderResult(IHttpActionResult nextActionResult, IEnumerable<Link> links) : base(nextActionResult)
        {
            if (links == null) throw new ArgumentNullException(nameof(links));
            _links = links;
        }

        protected override void ApplyActionResult(HttpResponseMessage response)
        {
            response.Headers.AddLinkHeader(_links);
        }
    }
}