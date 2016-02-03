using System;
using System.Linq;

namespace AnApiOfIceAndFire.Data.Feeder
{
    public class HouseDto : IEquatable<HouseDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Seats { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string[] Titles { get; set; }
        public int? CurrentLord { get; set; }
        public int? Founder { get; set; }
        public string Founded { get; set; }
        public int? Heir { get; set; }
        public int? Overlord { get; set; }
        public string DiedOut { get; set; }
        public string[] AncestralWeapons { get; set; }
        public int[] CadetBranches { get; set; }

        public bool Equals(HouseDto other)
        {
            if (other == null) return false;

            ////Note that SequenceEqual will return false if the items are the same but in different orders.
            return Id == other.Id
                   && string.Equals(Name, other.Name)
                   && Seats.SequenceEqual(other.Seats)
                   && string.Equals(Region, other.Region)
                   && string.Equals(CoatOfArms, other.CoatOfArms)
                   && string.Equals(Words, other.Words)
                   && Titles.SequenceEqual(other.Titles)
                   && CurrentLord == other.CurrentLord
                   && Founder == other.Founder
                   && string.Equals(Founded, other.Founded)
                   && Heir == other.Heir
                   && Overlord == other.Overlord
                   && string.Equals(DiedOut, other.DiedOut)
                   && AncestralWeapons.SequenceEqual(other.AncestralWeapons)
                   && CadetBranches.SequenceEqual(other.CadetBranches);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HouseDto);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + Id.GetHashCode();
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + Seats.GetHashCode();
            hash = hash*23 + Region.GetHashCode();
            hash = hash * 23 + CoatOfArms.GetHashCode();
            hash = hash * 23 + Words.GetHashCode();
            hash = hash * 23 + Titles.GetHashCode();
            hash = hash * 23 + CurrentLord.GetHashCode();
            hash = hash * 23 + Founder.GetHashCode();
            hash = hash * 23 + Founded.GetHashCode();
            hash = hash * 23 + Heir.GetHashCode();
            hash = hash * 23 + Overlord.GetHashCode();
            hash = hash * 23 + DiedOut.GetHashCode();
            hash = hash * 23 + AncestralWeapons.GetHashCode();
            hash = hash * 23 + CadetBranches.GetHashCode();

            return hash;
        }
    }
}