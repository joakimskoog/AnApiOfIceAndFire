using System;
using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class MediaTypeMapper : IModelMapper<Domain.Models.MediaType, MediaType>
    {
        public MediaType Map(Domain.Models.MediaType input, UrlHelper urlHelper)
        {
            switch (input)
            {
                case Domain.Models.MediaType.Hardcover: return MediaType.Hardcover;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public string CreateModelLink(Domain.Models.MediaType input, UrlHelper urlHelper)
        {
            throw new NotImplementedException("MediaType has no external endpoint");
        }
    }
}