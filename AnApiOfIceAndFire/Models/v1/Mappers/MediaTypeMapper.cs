using System;
using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class MediaTypeMapper : IModelMapper<Domain.Books.MediaType, MediaType>
    {
        public MediaType Map(Domain.Books.MediaType input, UrlHelper urlHelper)
        {
            switch (input)
            {
                case Domain.Books.MediaType.Hardback: return MediaType.Hardback;
                case Domain.Books.MediaType.Hardcover: return MediaType.Hardcover;
                case Domain.Books.MediaType.GraphicNovel: return MediaType.GraphicNovel;
                case Domain.Books.MediaType.Paperback: return MediaType.Paperback;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}