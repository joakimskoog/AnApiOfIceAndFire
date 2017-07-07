using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseEntity : BaseEntity
    {
        public string CoatOfArms { get; internal set; }
        public string Words { get; internal set; }
        public string Region { get; internal set; }
        public string Founded { get; internal set; }
        public string DiedOut { get; internal set; }

        internal string SeatsRaw { get; set; }
        public string[] Seats
        {
            get => SeatsRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => SeatsRaw = string.Join(";", value);
        }

        internal string TitlesRaw { get; set; }
        public string[] Titles
        {
            get => TitlesRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => TitlesRaw = string.Join(";", value);
        }

        internal string AncestralWeaponsRaw { get; set; }
        public string[] AncestralWeapons
        {
            get => AncestralWeaponsRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => AncestralWeaponsRaw = string.Join(";", value);
        }

        public int? FounderId { get; internal set; }
        public int? CurrentLordId { get; internal set; }
        public int? HeirId { get; internal set; }
        public int? OverlordId { get; internal set; }
        
        public ICollection<int> SwornMemberIdentifiers { get; internal set; } = new List<int>();
        public ICollection<int> CadetBranchIdentifiers { get; internal set; } = new List<int>();
    }
}