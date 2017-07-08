using System;
using System.Linq;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Infrastructure.Links;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Models
{
    public class BookMapper : IModelMapper<BookEntity, Book>
    {
        public Book Map(BookEntity @from, IUrlHelper urlHelper)
        {
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var bookUrl = urlHelper.LinkToBook(from.Id);
            var characterUrls = from.CharacterIdentifiers.Select(urlHelper.LinkToCharacter);
            var povCharacterUrls = from.PovCharacterIdentifiers.Select(urlHelper.LinkToCharacter);

            return new Book(bookUrl, from.Name, from.ISBN, from.Authors, from.NumberOfPages, from.Publisher, from.Country, from.MediaType, from.ReleaseDate, characterUrls, povCharacterUrls);
        }
    }
}