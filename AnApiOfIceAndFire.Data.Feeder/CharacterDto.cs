using System;
using System.Linq;

namespace AnApiOfIceAndFire.Data.Feeder
{
    public class CharacterDto : IEquatable<CharacterDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
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


        public bool Equals(CharacterDto other)
        {
            if (other == null) return false;

            ////Note that SequenceEqual will return false if the items are the same but in different orders.
            return Id == other.Id
                   && string.Equals(Name, other.Name)
                   && string.Equals(Culture, other.Culture)
                   && Titles.SequenceEqual(other.Titles) 
                   && OtherTitles.SequenceEqual(other.OtherTitles)
                   && Aliases.SequenceEqual(other.Aliases)
                   && string.Equals(Born, other.Born)
                   && string.Equals(Died, other.Died)
                   && Father == other.Father
                   && Mother == other.Mother
                   && Spouse == other.Spouse
                   && Children.SequenceEqual(other.Children)
                   && Allegiances.SequenceEqual(other.Allegiances)
                   && Books.SequenceEqual(other.Books)
                   && PovBooks.SequenceEqual(other.PovBooks)
                   && PlayedBy.SequenceEqual(other.PlayedBy)
                   && TvSeries.SequenceEqual(other.TvSeries);
        }

        public override bool Equals(object obj)
        {
           return Equals(obj as CharacterDto);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + Id.GetHashCode();
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + Culture.GetHashCode();
            hash = hash * 23 + Titles.GetHashCode();
            hash = hash * 23 + OtherTitles.GetHashCode();
            hash = hash * 23 + Aliases.GetHashCode();
            hash = hash * 23 + Born.GetHashCode();
            hash = hash * 23 + Died.GetHashCode();
            hash = hash * 23 + Father.GetHashCode();
            hash = hash * 23 + Mother.GetHashCode();
            hash = hash * 23 + Spouse.GetHashCode();
            hash = hash * 23 + Children.GetHashCode();
            hash = hash * 23 + Allegiances.GetHashCode();
            hash = hash * 23 + Books.GetHashCode();
            hash = hash * 23 + PovBooks.GetHashCode();
            hash = hash * 23 + PlayedBy.GetHashCode();
            hash = hash * 23 + TvSeries.GetHashCode();

            return hash;
        }
    }
}