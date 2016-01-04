using System;

namespace AnApiOfIceAndFire.Models.v0
{
    public static class MediaTypeMapping
    {
        public static MediaType Map(this Domain.Models.MediaType mediaType)
        {
            switch (mediaType)
            {
                case Domain.Models.MediaType.Hardcover:return MediaType.Hardcover;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}