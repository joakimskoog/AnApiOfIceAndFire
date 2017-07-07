using System;

namespace AnApiOfIceAndFire.Models
{
    public class EndpointsModel
    {
        /// <summary>
        /// The URL to the Books resource
        /// </summary>
        public string Books { get; }

        /// <summary>
        /// The URL to the Characters resource
        /// </summary>
        public string Characters { get; }

        /// <summary>
        /// The URL to the Houses resource
        /// </summary>
        public string Houses { get; }

        public EndpointsModel(string booksUrl, string charactersUrl, string housesUrl)
        {
            if (string.IsNullOrEmpty(booksUrl)) throw new ArgumentException(nameof(booksUrl));
            if (string.IsNullOrEmpty(charactersUrl)) throw new ArgumentException(nameof(charactersUrl));
            if (string.IsNullOrEmpty(housesUrl)) throw new ArgumentException(nameof(housesUrl));
            Books = booksUrl;
            Characters = charactersUrl;
            Houses = housesUrl;
        }
    }
}