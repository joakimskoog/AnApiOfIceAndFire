using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Models.v1
{
    public class Book
    {
        /// <summary>
        /// The hypermedia URL of this resource
        /// </summary>
        public string URL { get; }

        /// <summary>
        /// The name of this book
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The International Standard Book Number that uniquely identifies this book. The format used is ISBN-13.
        /// </summary>
        public string ISBN { get; }

        /// <summary>
        /// The authors of this book
        /// </summary>
        public IEnumerable<string> Authors { get; }

        /// <summary>
        /// The number of pages in this book
        /// </summary>
        public int NumberOfPages { get; }

        /// <summary>
        /// The publisher of this book
        /// </summary>
        public string Publisher { get; }

        /// <summary>
        /// The country that this book was published in
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// The media type of this book
        /// </summary>
        public MediaType MediaType { get; }

        /// <summary>
        /// The date, in ISO 8601 format, this book was released
        /// </summary>
        public DateTime Released { get; }

        /// <summary>
        /// The character resource URLs that are in this book
        /// </summary>
        public IEnumerable<string> Characters { get; }

        /// <summary>
        /// The character resource URLs that have at least one POV chapter in this book
        /// </summary>
        public IEnumerable<string> POVCharacters { get; }

        public Book(string url, string name, string isbn, IEnumerable<string> authors, int numberOfPages, string publisher, 
            string country, MediaType mediaType, DateTime released, IEnumerable<string> characters = null, IEnumerable<string> povCharacters = null)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (isbn == null) throw new ArgumentNullException(nameof(isbn));
            if (authors == null) throw new ArgumentNullException(nameof(authors));
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            if (country == null) throw new ArgumentNullException(nameof(country));
            URL = url;
            Name = name;
            ISBN = isbn;
            Authors = authors;
            NumberOfPages = numberOfPages;
            Publisher = publisher;
            Country = country;
            MediaType = mediaType;
            Released = released;
            Characters = characters ?? new List<string>();
            POVCharacters = povCharacters ?? new List<string>();
        }
    }
}