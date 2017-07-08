using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AnApiOfIceAndFire.Data.Characters;
using Dapper.Contrib.Extensions;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookEntity : BaseEntity
    {
        public string ISBN { get; set; }

        internal string AuthorsRaw { get; set; } = "";

        public string[] Authors
        {
            get => AuthorsRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            internal set => AuthorsRaw = string.Join(";", value);
        }
        
        public int NumberOfPages { get;  set; }
        public string Publisher { get;  set; }
        public MediaType MediaType { get;  set; }
        public string Country { get;  set; }
        public DateTime ReleaseDate { get;  set; }

        public ICollection<int> CharacterIdentifiers { get; set; } = new List<int>();

        public ICollection<int> PovCharacterIdentifiers { get; set; } = new List<int>();
    }
}