using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

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
            var bookUrl = urlHelper.Link("BooksApi", new { id = input.Identifier });
            var characterUrls = MapToCharactersUrl(input.Characters, urlHelper);
            var povCharacterUrls = MapToCharactersUrl(input.POVCharacters, urlHelper);
            var mediaType = _mediaTypeMapper.Map(input.MediaType, urlHelper);

            return new Book(bookUrl, input.Name, input.ISBN, input.Authors, input.NumberOfPages, input.Publisher,
                input.Country, mediaType, input.Released, characterUrls, povCharacterUrls);
        }

        private IEnumerable<string> MapToCharactersUrl(IEnumerable<ICharacter> characters, UrlHelper urlHelper)
        {
            if (characters == null) return Enumerable.Empty<string>();

            return characters.Select(c => urlHelper.Link("CharactersApi", new { id = c.Identifier }));
        }
    }
}