using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace AnApiOfIceAndFire.Data.Entities
{
    public class CharacterEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }

        public string AliasesRaw { get; set; }
        [NotMapped]
        public string[] Aliases
        {
            get { return AliasesRaw.Split(';'); }
            set { AliasesRaw = string.Join(";", value); }      
        }

        public string TitlesRaw { get; set; }
        [NotMapped]
        public string[] Titles
        {
            get { return TitlesRaw.Split(';'); }
            set { TitlesRaw = string.Join(";", value); }
        }

        public string TvSeriesRaw { get; set; }
        [NotMapped]
        public string[] TvSeries
        {
            get { return TvSeriesRaw.Split(';'); }
            set { TvSeriesRaw = string.Join(";", value); }
        }

        public string PlayedByRaw { get; set; }
        [NotMapped]
        public string[] PlayedBy
        {
            get { return PlayedByRaw.Split(';'); }
            set { PlayedByRaw = string.Join(";", value); }
        }

        public int? FatherId { get; set; }
        public CharacterEntity Father { get; set; }

        public int? MotherId { get; set; }
        public CharacterEntity Mother { get; set; }

        public int? SpouseId { get; set; }
        public CharacterEntity Spouse { get; set; }

        [InverseProperty("SwornMembers")]
        public ICollection<HouseEntity> Allegiances { get; set; } = new List<HouseEntity>();

        [InverseProperty("Characters")]
        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();

        [InverseProperty("PovCharacters")]
        public ICollection<BookEntity> PovBooks { get; set; } = new List<BookEntity>();
    }
}

