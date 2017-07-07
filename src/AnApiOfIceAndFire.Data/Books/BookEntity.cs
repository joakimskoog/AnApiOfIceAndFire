using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Data.Characters;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookEntity : BaseEntity
    {
        public string ISBN { get; internal set; }

        internal string AuthorsRaw { get; set; }
        public string[] Authors
        {
            get => AuthorsRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => AuthorsRaw = string.Join(";", value);
        }

        public int NumberOfPages { get; internal set; }
        public string Publisher { get; internal set; }
        public MediaType MediaType { get; internal set; }
        public string Country { get; internal set; }
        public DateTime ReleaseDate { get; internal set; }

        public ICollection<int> CharacterIdentifiers { get; set; } = new List<int>();
        public ICollection<int> PovCharacterIdentifiers { get; set; } = new List<int>();
    }
}