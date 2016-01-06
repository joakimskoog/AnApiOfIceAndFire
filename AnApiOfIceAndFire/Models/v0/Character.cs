using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Models.v0
{
    public class Character
    {
        public string URL { get; }
        public string Name { get; set; }
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public IEnumerable<string> Titles { get; set; }
        public IEnumerable<string> Aliases { get; set; }

        public string Father { get; set; }
        public string Mother { get; set; }
        public IEnumerable<string> Children { get; set; }
        public string Spouse { get; set; }

        public IEnumerable<string> Allegiances { get; set; }

        public IEnumerable<string> Books { get; set; }
        public IEnumerable<string> PovBooks { get; set; }

        public IEnumerable<string> TvSeries { get; set; }
        public IEnumerable<string> PlayedBy { get; set; }

        public Character(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            URL = url;
        }
    }
}