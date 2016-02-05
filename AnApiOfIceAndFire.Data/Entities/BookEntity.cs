using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; }

        public string AuthorsRaw { get; set; }
        public string[] Authors
        {
            get { return AuthorsRaw.Split(';'); }
            set { AuthorsRaw = string.Join(";", value); }
        }

        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public MediaTypeEntity MediaType { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ICollection<CharacterEntity> Characters { get; set; } = new List<CharacterEntity>();
        public ICollection<CharacterEntity> PovCharacters { get; set; } = new List<CharacterEntity>();
    }
}