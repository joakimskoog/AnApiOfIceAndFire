using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AnApiOfIceAndFire.Infrastructure.Html
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString Markdown(this HtmlHelper helper, string path)
        {
            var mappedPath = HttpContext.Current.Server.MapPath(path);
            var markdownContent = File.ReadAllText(mappedPath);
            var htmlContent = CommonMark.CommonMarkConverter.Convert(markdownContent);

            return new MvcHtmlString(htmlContent);
        }
    }
}