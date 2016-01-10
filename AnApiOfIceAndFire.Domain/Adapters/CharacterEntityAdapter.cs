using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Adapters
{
    public class CharacterEntityAdapter : ICharacter
    {
        private readonly CharacterEntity _characterEntity;

        public int Identifier => _characterEntity.Id;
        public string Name { get; }
        public string Culture { get; }
        public string Born { get; }
        public string Died { get; }
        public IReadOnlyCollection<string> Titles { get; }
        public IReadOnlyCollection<string> Aliases { get; }
        public ICharacter Father { get; }
        public ICharacter Mother { get; }
        public IReadOnlyCollection<ICharacter> Children { get; }
        public ICharacter Spouse { get; }
        public IReadOnlyCollection<IHouse> Allegiances { get; }
        public IReadOnlyCollection<IBook> Books { get; }
        public IReadOnlyCollection<IBook> PovBooks { get; }
        public IReadOnlyCollection<string> TvSeries { get; }
        public IReadOnlyCollection<string> PlayedBy { get; }

        public CharacterEntityAdapter(CharacterEntity characterEntity)
        {
            if (characterEntity == null) throw new ArgumentNullException(nameof(characterEntity));
            _characterEntity = characterEntity;
        }
    }
}