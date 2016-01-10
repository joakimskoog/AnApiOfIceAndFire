using System;
using EntityFrameworkRepository.Entities;
// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        //MediaType
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
