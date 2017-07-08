using System;

namespace AnApiOfIceAndFire.DataFeeder
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string MediaType { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? PrecededBy { get; set; }
        public int? FollowedBy { get; set; }
    }
}


//public string ISBN { get; set; }

//internal string AuthorsRaw { get; set; }
//public string[] Authors
//{
//get => AuthorsRaw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
//internal set => AuthorsRaw = string.Join(";", value);
//}

//public int NumberOfPages { get; set; }
//public string Publisher { get; set; }
//public MediaType MediaType { get; set; }
//public string Country { get; set; }
//public DateTime ReleaseDate { get; set; }

//public ICollection<int> CharacterIdentifiers { get; set; } = new List<int>();
//public ICollection<int> PovCharacterIdentifiers { get; set; } = new List<int>();