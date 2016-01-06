using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Models.v0
{
    public static class BookLinkCreator
    {
        public const string BookRouteName = "BooksApi";

        public static string CreateBookLink(IBook book, UrlHelper urlHelper)
        {
            if (book == null) return string.Empty;

            return urlHelper.Link(BookRouteName, new {id = book.Identifier});
        }
    }
}