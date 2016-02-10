using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Entities
{
    public class CharacterEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }

        public string AliasesRaw { get; set; }
        public string[] Aliases
        {
            get { return AliasesRaw.Split(new [] {';'}, StringSplitOptions.RemoveEmptyEntries); }
            set { AliasesRaw = string.Join(";", value); }      
        }

        public string TitlesRaw { get; set; }
        public string[] Titles
        {
            get { return TitlesRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries); }
            set { TitlesRaw = string.Join(";", value); }
        }

        public string TvSeriesRaw { get; set; }
        public string[] TvSeries
        {
            get { return TvSeriesRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries); }
            set { TvSeriesRaw = string.Join(";", value); }
        }

        public string PlayedByRaw { get; set; }
        public string[] PlayedBy
        {
            get { return PlayedByRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries); }
            set { PlayedByRaw = string.Join(";", value); }
        }

        public int? FatherId { get; set; }
        public CharacterEntity Father { get; set; }

        public int? MotherId { get; set; }
        public CharacterEntity Mother { get; set; }

        public int? SpouseId { get; set; }
        public CharacterEntity Spouse { get; set; }

        public ICollection<HouseEntity> Allegiances { get; set; } = new List<HouseEntity>();

        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
        public ICollection<BookEntity> PovBooks { get; set; } = new List<BookEntity>();
    }
}

