using System;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data.Feeder
{
    public class BookDto : IEquatable<BookDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string MediaType { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? PrecededBy { get; set; }
        public int? FollowedBy { get; set; }

        public bool Equals(BookDto other)
        {
            if (other == null) return false;

            return Id == other.Id
                   && string.Equals(Name, other.Name)
                   && string.Equals(ISBN, other.ISBN)
                   && Authors.SequenceEqual(other.Authors) //Note that this will return false if there are same authors, but in different order.
                   && NumberOfPages == other.NumberOfPages
                   && string.Equals(Publisher, other.Publisher)
                   && string.Equals(MediaType, other.MediaType)
                   && string.Equals(Country, other.Country)
                   && ReleaseDate == other.ReleaseDate
                   && PrecededBy == other.PrecededBy
                   && FollowedBy == other.FollowedBy;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BookDto);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + Id.GetHashCode();
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + Authors.GetHashCode();
            hash = hash * 23 + NumberOfPages.GetHashCode();
            hash = hash * 23 + Publisher.GetHashCode();
            hash = hash * 23 + MediaType.GetHashCode();
            hash = hash * 23 + Country.GetHashCode();
            hash = hash * 23 + ReleaseDate.GetHashCode();
            hash = hash * 23 + PrecededBy.GetHashCode();
            hash = hash * 23 + FollowedBy.GetHashCode();

            return hash;
        }
    }
}