using System;
using System.Text;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public static class LinkExtensions
    {
        public static string ToLinkHeader(this Link link)
        {
            if (link == null) throw new ArgumentNullException(nameof(link));

            var builder = new StringBuilder($"<{link.Target.OriginalString}>");

            if (!string.IsNullOrEmpty(link.Relation))
            {
                AppendParameter("rel", link.Relation, builder);
                //builder.Append(";");
                //builder.Append("rel=");
                //builder.Append('"');
                //builder.Append(link.Relation);
                //builder.Append('"');
            }

            return builder.ToString();
        }

        private static void AppendParameter(string name, string value, StringBuilder builder)
        {
            builder.Append("; ");
            builder.Append($"{name}=\"{value}\"");
        }
    }
}