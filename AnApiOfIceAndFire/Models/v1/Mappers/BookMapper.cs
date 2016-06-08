using System;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Books;
using MediaType = AnApiOfIceAndFire.Models.v1.MediaType;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public class BookMapper : IModelMapper<IBook, Book>
    {
        private readonly IModelMapper<Domain.Books.MediaType, MediaType> _mediaTypeMapper;

        public BookMapper(IModelMapper<Domain.Books.MediaType, MediaType> mediaTypeMapper)
        {
            if (mediaTypeMapper == null) throw new ArgumentNullException(nameof(mediaTypeMapper));
            _mediaTypeMapper = mediaTypeMapper;
        }

        public Book Map(IBook input, UrlHelper urlHelper)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var bookUrl = BookLinkCreator.CreateBookLink(input, urlHelper);
            var characterUrls = input.Characters?.Select(c => CharacterLinkCreator.CreateCharacterLink(c, urlHelper));
            var povCharacterUrls = input.POVCharacters?.Select(c => CharacterLinkCreator.CreateCharacterLink(c, urlHelper));
            var mediaType = _mediaTypeMapper.Map(input.MediaType, urlHelper);

            return new Book(bookUrl, input.Name, input.ISBN, input.Authors, input.NumberOfPages, input.Publisher,
                input.Country, mediaType, input.Released, characterUrls, povCharacterUrls);
        }
    }
}