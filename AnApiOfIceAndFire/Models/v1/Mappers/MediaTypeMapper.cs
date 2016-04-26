using System;
using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class MediaTypeMapper : IModelMapper<Domain.Models.MediaType, MediaType>
    {
        public MediaType Map(Domain.Models.MediaType input, UrlHelper urlHelper)
        {
            switch (input)
            {
                case Domain.Models.MediaType.Hardback: return MediaType.Hardback;
                case Domain.Models.MediaType.Hardcover: return MediaType.Hardcover;
                case Domain.Models.MediaType.GraphicNovel: return MediaType.GraphicNovel;
                case Domain.Models.MediaType.Paperback: return MediaType.Paperback;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}