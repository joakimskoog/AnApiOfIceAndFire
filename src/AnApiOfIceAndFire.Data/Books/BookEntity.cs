using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace AnApiOfIceAndFire.Data.Books
{
    [Table("books")]
    public class BookEntity : BaseEntity
    {
        public string ISBN { get; set; }
        public string Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public MediaType MediaType { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }

        [Computed]
        public ICollection<int> CharacterIdentifiers { get; internal set; } = new List<int>();

        [Computed]
        public ICollection<int> PovCharacterIdentifiers { get; internal set; } = new List<int>();

        public string[] ParseAuthors()
        {
            return Authors.Split(new[] { SplitDelimiter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}