using System;
using System.Collections.Generic;
using System.Linq;
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

        private MediaType? _mediaType;
        public MediaType MediaType
        {
            get
            {
                if (!_mediaType.HasValue)
                {
                    _mediaType = _bookEntity.MediaType.MapToMediatype();
                }

                return _mediaType.Value;
            }
        }

        public DateTime Released => _bookEntity.ReleaseDate;

        private IReadOnlyCollection<ICharacter> _characters;
        public IReadOnlyCollection<ICharacter> Characters
        {
            get
            {
                return _characters ?? (_characters = _bookEntity.Characters.Select(c => new CharacterEntityAdapter(c)).ToList());
            }
        }

        private IReadOnlyCollection<ICharacter> _povCharacters;
        public IReadOnlyCollection<ICharacter> POVCharacters
        {
            get
            {
                return _povCharacters ?? (_povCharacters = _bookEntity.PovCharacters.Select(c => new CharacterEntityAdapter(c)).ToList());
            }
        }

        public BookEntityAdapter(BookEntity bookEntity)
        {
            if (bookEntity == null) throw new ArgumentNullException(nameof(bookEntity));
            _bookEntity = bookEntity;
        }
    }
}