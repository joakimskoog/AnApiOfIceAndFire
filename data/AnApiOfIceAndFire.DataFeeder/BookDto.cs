using System;
using AnApiOfIceAndFire.Data.Books;

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

        public BookEntity ToBookEntity()
        {
            return new BookEntity()
            {
                Id = Id,
                Name = Name,
                ISBN = ISBN,
                Country = Country,
                NumberOfPages = NumberOfPages,
                Publisher = Publisher,
                ReleaseDate = ReleaseDate,
                MediaType = MediaTypeParser.ParseMediaType(MediaType),
                Authors = string.Join(";", Authors)
            };
        }
    }
}