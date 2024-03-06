using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterModel : BaseModel
    {
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public Gender Gender { get; set; }
        public string Aliases { get; set; }
        public string Titles { get; set; }
        public string TvSeries { get; set; }
        public string PlayedBy { get; set; }

        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public int? SpouseId { get; set; }

        public ICollection<int> AllegianceIdentifiers { get; set; } = new List<int>();
        public ICollection<int> BookIdentifiers { get; set; } = new List<int>();
        public ICollection<int> PovBookIdentifiers { get; set; } = new List<int>();
    }

    public enum Gender
    {
        Unknown = 0,
        Female = 1,
        Male = 2
    }
}
