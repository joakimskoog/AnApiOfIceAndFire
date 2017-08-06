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
            if (from == null) return null;
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            var bookUrl = urlHelper.LinkToBook(from.Id);
            var characterUrls = from.CharacterIdentifiers.Select(urlHelper.LinkToCharacter).ToArray();
            var povCharacterUrls = from.PovCharacterIdentifiers.Select(urlHelper.LinkToCharacter).ToArray();

            return new Book(bookUrl, from.Name, from.ISBN, from.ParseAuthors(), from.NumberOfPages, from.Publisher,
                from.Country, from.MediaType, from.ReleaseDate, characterUrls, povCharacterUrls);
        }
    }
}