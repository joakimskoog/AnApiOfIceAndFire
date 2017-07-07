using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterEntity : BaseEntity
    {
        public string Culture { get; internal set; }
        public string Born { get; internal set; }
        public string Died { get; internal set; }
        public bool? IsFemale { get; internal set; }

        internal string AliasesRaw { get; set; }
        public string[] Aliases
        {
            get => AliasesRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => AliasesRaw = string.Join(";", value);
        }

        internal string TitlesRaw { get; set; }
        public string[] Titles
        {
            get => TitlesRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => TitlesRaw = string.Join(";", value);
        }

        internal string TvSeriesRaw { get; set; }
        public string[] TvSeries
        {
            get => TvSeriesRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => TvSeriesRaw = string.Join(";", value);
        }

        internal string PlayedByRaw { get; set; }
        public string[] PlayedBy
        {
            get => PlayedByRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => PlayedByRaw = string.Join(";", value);
        }

        public int? FatherId { get; internal set; }
        public int? MotherId { get; internal set; }
        public int? SpouseId { get; internal set; }
        
        public ICollection<int> AllegianceIdentifiers { get; internal set; } = new List<int>();

        public ICollection<int> BookIdentifiers { get; internal set; } = new List<int>();
        public ICollection<int> PovBookIdentifiers { get; internal set; } = new List<int>();
    }
}