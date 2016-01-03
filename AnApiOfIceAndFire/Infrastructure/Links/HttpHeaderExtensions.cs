using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public static class HttpHeaderExtensions
    {
        public static void AddLinkHeader(this HttpHeaders headers, Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));
            var headerValue = link.ToLinkHeader();
            headers.Add("Link", headerValue);
        }

        public static void AddLinkHeader(this HttpHeaders headers, IList<Link> links)
        {
            if (links == null) throw new ArgumentNullException(nameof(links));

            string headerValue = string.Empty;
            foreach (var link in links)
            {
                headerValue += link.ToLinkHeader();
                headerValue += ", ";
            }

            headerValue = headerValue.Substring(0, headerValue.Length - 2);

            headers.Add("Link", headerValue);
        }
    }
}