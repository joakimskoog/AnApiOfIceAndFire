using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Books
{
    public class BookModel : BaseModel
    {
        public string ISBN { get; set; }
        public string Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public MediaType MediaType { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ICollection<int> CharacterIdentifiers { get; internal set; } = new List<int>();
        public ICollection<int> PovCharacterIdentifiers { get; internal set; } = new List<int>();
    }

    internal class BookCharacter
    {
        public int BookId { get; set; }
        public int CharacterId { get; set; }
        public int Type { get; set; }
    }

    public enum MediaType
    {
        Hardcover = 0,
        Hardback = 1,
        GraphicNovel = 2,
        Paperback = 3,
    }
}
