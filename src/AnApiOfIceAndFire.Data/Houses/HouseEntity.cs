using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace AnApiOfIceAndFire.Data.Houses
{
    [Table("houses")]
    public class HouseEntity : BaseEntity
    {
        public string CoatOfArms { get;  set; }
        public string Words { get;  set; }
        public string Region { get;  set; }
        public string Founded { get;  set; }
        public string DiedOut { get;  set; }

        public string Seats { get; set; }
        public string Titles { get; set; }
        public string AncestralWeapons { get; set; }

        public int? FounderId { get;  set; }
        public int? CurrentLordId { get;  set; }
        public int? HeirId { get;  set; }
        public int? OverlordId { get;  set; }

        [Computed]
        public ICollection<int> SwornMemberIdentifiers { get;  set; } = new List<int>();

        [Computed]
        public ICollection<int> CadetBranchIdentifiers { get;  set; } = new List<int>();

        public string[] ParseSeats()
        {
            return Seats.Split(SplitDelimiter);
        }

        public string[] ParseTitles()
        {
            return Titles.Split(SplitDelimiter);
        }

        public string[] ParseAncestralWeapons()
        {
            return AncestralWeapons.Split(SplitDelimiter);
        }
    }
}