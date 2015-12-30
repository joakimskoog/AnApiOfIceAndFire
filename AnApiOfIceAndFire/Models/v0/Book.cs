using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.Models.v0
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
        public string[] Authors { get; }

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
        public IReadOnlyCollection<string> Characters { get; } 

        /// <summary>
        /// The character resource URLs that have at least one POV chapter in this book
        /// </summary>
        public IReadOnlyCollection<string> POVCharacters { get; } 
    }
}