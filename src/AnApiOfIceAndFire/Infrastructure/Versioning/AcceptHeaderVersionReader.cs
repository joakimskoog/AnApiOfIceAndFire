using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Primitives;

namespace AnApiOfIceAndFire.Infrastructure.Versioning
{
    /// <summary>
    /// An implementation of <see cref="IApiVersionReader"/> that reads the version from the accept header.
    /// </summary>
    public class AcceptHeaderVersionReader : IApiVersionReader
    {
        public const string MediaType = "application/vnd.anapioficeandfire+json";
        public const string MediaTypeParameter = "version";

        public void AddParameters(IApiVersionParameterDescriptionContext context)
        {
            context.AddParameter(MediaTypeParameter, ApiVersionParameterLocation.MediaTypeParameter);
        }

        public string Read(HttpRequest request)
        {
            var acceptHeader = request?.GetTypedHeaders()?.Accept;
            if (acceptHeader == null)
            {
                return null;
            }

            foreach (var mime in acceptHeader.OrderByDescending(m => m.Quality))
            {
                if (StringSegment.Equals(mime.MediaType, MediaType, StringComparison.OrdinalIgnoreCase))
                {
                    var versionParameter = mime.Parameters.FirstOrDefault(p => StringSegment.Equals(p.Name, MediaTypeParameter, StringComparison.OrdinalIgnoreCase));

                    var version = versionParameter?.Value;
                    if (version == null || StringSegment.Equals(version, StringSegment.Empty))
                    {
                        return null;
                    }

                    return version.ToString();
                }
            }

            return null;
        }
    }
}