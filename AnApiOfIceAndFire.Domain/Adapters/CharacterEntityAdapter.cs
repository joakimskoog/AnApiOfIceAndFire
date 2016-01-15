using System;
using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Adapters
{
    public class CharacterEntityAdapter : ICharacter
    {
        private readonly CharacterEntity _characterEntity;

        public int Identifier => _characterEntity.Id;
        public string Name => _characterEntity.Name;
        public string Culture => _characterEntity.Culture;
        public string Born => _characterEntity.Born;
        public string Died => _characterEntity.Died;
        public IReadOnlyCollection<string> Titles => _characterEntity.Titles;
        public IReadOnlyCollection<string> Aliases => _characterEntity.Aliases;

        private ICharacter _father;
        public ICharacter Father
        {
            get
            {
                if (_characterEntity.Father == null)
                {
                    return null;
                }

                return _father ?? (_father = new CharacterEntityAdapter(_characterEntity.Father));
            }
        }

        private ICharacter _mother;
        public ICharacter Mother
        {
            get
            {
                if (_characterEntity.Mother == null)
                {
                    return null;
                }

                return _mother ?? (_mother = new CharacterEntityAdapter(_characterEntity.Mother));
            }
        }
        public IReadOnlyCollection<ICharacter> Children { get; }

        private ICharacter _spouse;
        public ICharacter Spouse
        {
            get
            {
                if (_characterEntity.Spouse == null)
                {
                    return null;
                }

                return _spouse ?? (_spouse = new CharacterEntityAdapter(_characterEntity.Spouse));
            }
        }

        private IReadOnlyCollection<IHouse> _allegiances;
        public IReadOnlyCollection<IHouse> Allegiances
        {
            get
            {
                return _allegiances ?? (_allegiances = _characterEntity.Allegiances.Select(h => new HouseEntityAdapter(h)).ToList());
            }
        }

        private IReadOnlyCollection<IBook> _books;
        public IReadOnlyCollection<IBook> Books
        {
            get { return _books ?? (_books = _characterEntity.Books.Select(b => new BookEntityAdapter(b)).ToList()); }
        }

        private IReadOnlyCollection<IBook> _povBooks;
        public IReadOnlyCollection<IBook> PovBooks
        {
            get
            {
                return _povBooks ?? (_povBooks = _characterEntity.PovBooks.Select(b => new BookEntityAdapter(b)).ToList());
            }
        }

        public IReadOnlyCollection<string> TvSeries => _characterEntity.TvSeries;
        public IReadOnlyCollection<string> PlayedBy => _characterEntity.PlayedBy;

        public CharacterEntityAdapter(CharacterEntity characterEntity)
        {
            if (characterEntity == null) throw new ArgumentNullException(nameof(characterEntity));
            _characterEntity = characterEntity;
        }
    }
}