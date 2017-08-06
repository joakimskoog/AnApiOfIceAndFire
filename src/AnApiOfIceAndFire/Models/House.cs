using System;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Models
{
    public class House
    {
        public string URL { get; }
        public string Name { get; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string[] Titles { get; set; }
        public string[] Seats { get; set; }
        public string CurrentLord { get; set; }
        public string Heir { get; set; }
        public string Overlord { get; set; }
        public string Founded { get; set; }
        public string Founder { get; set; }
        public string DiedOut { get; set; }
        public string[] AncestralWeapons { get; set; }
        public string[] CadetBranches { get; set; }
        public string[] SwornMembers { get; set; }

        public House(string url, string name)
        {
            URL = url ?? throw new ArgumentNullException(nameof(url));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}