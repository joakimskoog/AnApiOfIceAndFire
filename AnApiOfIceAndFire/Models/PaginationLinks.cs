using System;

namespace AnApiOfIceAndFire.Models
{
    public class PaginationLinks
    {
        /// <summary>
        /// The first page of data
        /// </summary>
        public string First { get; }

        /// <summary>
        /// The last page of data
        /// </summary>
        public string Last { get; }

        /// <summary>
        /// The current page of data
        /// </summary>
        public string Self { get; }

        /// <summary>
        /// The previous page of data
        /// </summary>
        public string Previous { get; }

        /// <summary>
        /// The next page of data
        /// </summary>
        public string Next { get; }

        public PaginationLinks(string first, string last, string self, string previous = null, string next = null)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (last == null) throw new ArgumentNullException(nameof(last));
            if (self == null) throw new ArgumentNullException(nameof(self));
            First = first;
            Last = last;
            Self = self;
            Previous = previous;
            Next = next;
        }
    }
}