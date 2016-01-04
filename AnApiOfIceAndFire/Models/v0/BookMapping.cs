using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v0
{
    public static class BookMapping
    {
        public static Book Map(this IBook book, UrlHelper urlHelper)
        {
            var bookUrl = urlHelper.Link("BooksApi", new { id = book.Identifier });
            var characterUrls = MapToCharactersUrl(book.Characters, urlHelper);
            var povCharacterUrls = MapToCharactersUrl(book.POVCharacters, urlHelper);

            return new Book(bookUrl, book.Name, book.ISBN, book.Authors, book.NumberOfPages, book.Publisher,
                book.Country, book.MediaType.Map(), book.Released, characterUrls, povCharacterUrls);
        }

        private static IEnumerable<string> MapToCharactersUrl(IEnumerable<ICharacter> characters, UrlHelper urlHelper)
        {
            if (characters == null) return Enumerable.Empty<string>();

            return characters.Select(c => urlHelper.Link("CharactersApi", new {id = c.Identifier}));
        } 
    }
}