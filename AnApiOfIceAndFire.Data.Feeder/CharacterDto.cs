using System;
using System.Linq;

namespace AnApiOfIceAndFire.Data.Feeder
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsFemale { get; set; }
        public string Culture { get; set; }
        public string[] Titles { get; set; }
        public string[] OtherTitles { get; set; }
        public string[] Aliases { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public int? Father { get; set; }
        public int? Mother { get; set; }
        public int? Spouse { get; set; }
        public int[] Children { get; set; }
        public int[] Allegiances { get; set; }
        public int[] Books { get; set; }
        public int[] PovBooks { get; set; }
        public string[] PlayedBy { get; set; }
        public string[] TvSeries { get; set; }
    }
}