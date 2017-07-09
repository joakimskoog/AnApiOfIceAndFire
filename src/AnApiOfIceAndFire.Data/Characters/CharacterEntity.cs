using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace AnApiOfIceAndFire.Data.Characters
{
    [Table("characters")]
    public class CharacterEntity : BaseEntity
    {
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public bool IsFemale { get; set; }
        public string Aliases { get; set; }
        public string Titles { get; set; }
        public string TvSeries { get; set; }
        public string PlayedBy { get; set; }

        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public int? SpouseId { get; set; }

        [Computed]
        public ICollection<int> AllegianceIdentifiers { get;  set; } = new List<int>();

        [Computed]
        public ICollection<int> BookIdentifiers { get;  set; } = new List<int>();

        [Computed]
        public ICollection<int> PovBookIdentifiers { get;  set; } = new List<int>();

        public string[] ParseAliases()
        {
            return Aliases.Split(SplitDelimiter);
        }

        public string[] ParseTitles()
        {
            return Titles.Split(SplitDelimiter);
        }

        public string[] ParseTvSeries()
        {
            return TvSeries.Split(SplitDelimiter);
        }

        public string[] ParsePlayedBy()
        {
            return PlayedBy.Split(SplitDelimiter);
        }
    }
}