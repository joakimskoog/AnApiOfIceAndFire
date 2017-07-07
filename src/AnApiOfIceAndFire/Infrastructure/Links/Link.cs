using System;
using System.Text;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public class Link
    {
        public Uri Target { get; }
        public string Relation { get; set; }

        public Link(Uri target, string relation = "")
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Relation = relation ?? throw new ArgumentNullException(nameof(relation));
        }

        public string ToLinkHeader()
        {
            var builder = new StringBuilder($"<{Target.OriginalString}>");

            if (!string.IsNullOrEmpty(Relation))
            {
                AppendParameter("rel", Relation, builder);
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