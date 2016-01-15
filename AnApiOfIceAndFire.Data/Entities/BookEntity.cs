using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; }

        public string AuthorsRaw { get; set; }

        [NotMapped]
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

        [InverseProperty("Books")]
        public ICollection<CharacterEntity> Characters { get; set; } = new List<CharacterEntity>();

        [InverseProperty("PovBooks")]
        public ICollection<CharacterEntity> PovCharacters { get; set; } = new List<CharacterEntity>();
    }
}