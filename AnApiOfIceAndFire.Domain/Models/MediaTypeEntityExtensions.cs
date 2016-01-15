using System;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Domain.Models
{
    public static class MediaTypeEntityExtensions
    {
        public static MediaType MapToMediatype(this MediaTypeEntity mediaType)
        {
            switch (mediaType)
            {
                case MediaTypeEntity.Hardback: return MediaType.Hardback;
                case MediaTypeEntity.Hardcover: return MediaType.Hardcover;
                case MediaTypeEntity.GraphicNovel: return MediaType.GraphicNovel;
                case MediaTypeEntity.Paperback: return MediaType.Paperback;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}