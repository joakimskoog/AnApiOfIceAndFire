using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnApiOfIceAndFire.Data.Entities
{
    public class HouseEntity : BaseEntity
    {
        public string Name { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string Region { get; set; }
        public string Founded { get; set; }
        public string DiedOut { get; set; }

        public string SeatsRaw { get; set; }
        public string[] Seats
        {
            get { return SeatsRaw.Split(','); }
            set { SeatsRaw = string.Join(",", value); }
        }

        public string TitlesRaw { get; set; }
        public string[] Titles
        {
            get { return TitlesRaw.Split(';'); }
            set { TitlesRaw = string.Join(";", value); }
        }

        public string AncestralWeaponsRaw { get; set; }
        public string[] AncestralWeapons
        {
            get { return AncestralWeaponsRaw.Split(';'); }
            set { AncestralWeaponsRaw = string.Join(";", value); }
        }

        public int? FounderId { get; set; }
        public CharacterEntity Founder { get; set; }

        public int? CurrentLordId { get; set; }
        public CharacterEntity CurrentLord { get; set; }

        public int? HeirId { get; set; }
        public CharacterEntity Heir { get; set; }

        public int? OverlordId { get; set; }
        public HouseEntity Overlord { get; set; }

        public ICollection<CharacterEntity> SwornMembers { get; set; } = new List<CharacterEntity>();
        public ICollection<HouseEntity> CadetBranches { get; set; } = new List<HouseEntity>();
    }
}