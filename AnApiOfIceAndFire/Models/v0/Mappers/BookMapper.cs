using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;
using static AnApiOfIceAndFire.Models.v0.BookLinkCreator;
using static AnApiOfIceAndFire.Models.v0.CharacterLinkCreator;

namespace AnApiOfIceAndFire.Models.v0.Mappers
{
    public class BookMapper : IModelMapper<IBook, Book>
    {
        private readonly IModelMapper<Domain.Models.MediaType, MediaType> _mediaTypeMapper;

        public BookMapper(IModelMapper<Domain.Models.MediaType, MediaType> mediaTypeMapper)
        {
            if (mediaTypeMapper == null) throw new ArgumentNullException(nameof(mediaTypeMapper));
            _mediaTypeMapper = mediaTypeMapper;
        }

        public Book Map(IBook input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var bookUrl = CreateBookLink(input, urlHelper);
            var characterUrls = input.Characters?.Select(c => CreateCharacterLink(c, urlHelper));
            var povCharacterUrls = input.POVCharacters?.Select(c => CreateCharacterLink(c, urlHelper));
            var mediaType = _mediaTypeMapper.Map(input.MediaType, urlHelper);

            return new Book(bookUrl, input.Name, input.ISBN, input.Authors, input.NumberOfPages, input.Publisher,
                input.Country, mediaType, input.Released, characterUrls, povCharacterUrls);
        }
    }
}