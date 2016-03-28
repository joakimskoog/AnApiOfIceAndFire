using System;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data.Feeder
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