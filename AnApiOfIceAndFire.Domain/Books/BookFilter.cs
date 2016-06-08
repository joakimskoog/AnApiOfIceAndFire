using System;

namespace AnApiOfIceAndFire.Domain.Books
{
    public class BookFilter
    {
        public string Name { get; set; }
        public DateTime? FromReleaseDate { get; set; }
        public DateTime? ToReleaseDate { get; set; }
    }
}