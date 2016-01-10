using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Adapters
{
    public class BookEntityAdapter : IBook
    {
        private readonly BookEntity _bookEntity;

        public int Identifier => _bookEntity.Id;
        public string Name => _bookEntity.Name;
        public string ISBN => _bookEntity.ISBN;
        public IReadOnlyCollection<string> Authors => _bookEntity.Authors;
        public int NumberOfPages => _bookEntity.NumberOfPages;
        public string Publisher => _bookEntity.Publisher;
        public string Country => _bookEntity.Country;
        public MediaType MediaType { get; }
        public DateTime Released => _bookEntity.ReleaseDate;
        public IReadOnlyCollection<ICharacter> Characters { get; }
        public IReadOnlyCollection<ICharacter> POVCharacters { get; }

        public BookEntityAdapter(BookEntity bookEntity)
        {
            if (bookEntity == null) throw new ArgumentNullException(nameof(bookEntity));
            _bookEntity = bookEntity;
        }
    }
}