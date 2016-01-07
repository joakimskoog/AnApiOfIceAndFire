using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Adapters
{
    public class BookEntityAdapter : IBook
    {
        private readonly BookEntity _bookEntity;

        public int Identifier => _bookEntity.Identifier;
        public string Name { get; }
        public string ISBN { get; }
        public IReadOnlyCollection<string> Authors { get; }
        public int NumberOfPages { get; }
        public string Publisher { get; }
        public string Country { get; }
        public MediaType MediaType { get; }
        public DateTime Released { get; }
        public IReadOnlyCollection<ICharacter> Characters { get; }
        public IReadOnlyCollection<ICharacter> POVCharacters { get; }

        public BookEntityAdapter(BookEntity bookEntity)
        {
            if (bookEntity == null) throw new ArgumentNullException(nameof(bookEntity));
            _bookEntity = bookEntity;
        }
    }
}