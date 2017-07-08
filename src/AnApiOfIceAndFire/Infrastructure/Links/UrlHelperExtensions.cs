using System;
using AnApiOfIceAndFire.Controllers.v1;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public static class UrlHelperExtensions
    {
        public static string LinkToBook(this IUrlHelper urlHelper, int? bookId)
        {
            return bookId.HasValue ? urlHelper.LinkToBook(bookId.Value) : "";
        }

        public static string LinkToBook(this IUrlHelper urlHelper, int bookId)
        {
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            return urlHelper.Link(BooksController.SingleBookRouteName, new { id = bookId });
        }

        public static string LinkToCharacter(this IUrlHelper urlHelper, int? characterId)
        {
            return characterId.HasValue ? urlHelper.LinkToCharacter(characterId.Value) : "";
        }

        public static string LinkToCharacter(this IUrlHelper urlHelper, int characterId)
        {
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            return urlHelper.Link(CharactersController.SingleCharacterRouteName, new { id = characterId });
        }

        public static string LinkToHouse(this IUrlHelper urlHelper, int? houseId)
        {
            return houseId.HasValue ? urlHelper.LinkToHouse(houseId.Value) : "";
        }

        public static string LinkToHouse(this IUrlHelper urlHelper, int houseId)
        {
            if (urlHelper == null) throw new ArgumentNullException(nameof(urlHelper));

            return urlHelper.Link(HousesController.SingleHouseRouteName, new { id = houseId });
        }
    }
}