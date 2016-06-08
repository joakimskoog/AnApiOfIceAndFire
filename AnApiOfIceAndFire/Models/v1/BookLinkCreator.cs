using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Books;

namespace AnApiOfIceAndFire.Models.v1
{
    public static class BookLinkCreator
    {
        public const string SingleBookRouteName = "SingleBooksApi";
        public const string MultipleBooksRouteName = "MultipleBooksApi";

        public static string CreateBookLink(IBook book, UrlHelper urlHelper)
        {
            if (book == null) return string.Empty;

            return urlHelper.Link(SingleBookRouteName, new {id = book.Identifier});
        }

        public static string CreateBooksLink(UrlHelper urlHelper)
        {
            if (urlHelper == null) return string.Empty;

            return urlHelper.Link(MultipleBooksRouteName, new {});
        }
    }
}