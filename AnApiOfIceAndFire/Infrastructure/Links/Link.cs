using System;

namespace AnApiOfIceAndFire.Infrastructure.Links
{
    public class Link
    {
        public Uri Target { get; }
        public string Relation { get; set; }

        public Link(Uri target, string relation = "")
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (relation == null) throw new ArgumentNullException(nameof(relation));
            Target = target;
            Relation = relation;
        }
    }
}