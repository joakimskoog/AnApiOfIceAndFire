using System;
using System.Collections.Generic;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Adapters
{
    public class HouseEntityAdapter : IHouse
    {
        private readonly HouseEntity _houseEntity;

        public int Identifier => _houseEntity.Identifier;
        public string Name { get; }
        public string Region { get; }
        public string CoatOfArms { get; }
        public string Words { get; }
        public IReadOnlyCollection<string> Titles { get; }
        public IReadOnlyCollection<string> Seats { get; }
        public ICharacter CurrentLord { get; }
        public ICharacter Heir { get; }
        public IHouse Overlord { get; }
        public string Founded { get; }
        public ICharacter Founder { get; }
        public string DiedOut { get; }
        public IReadOnlyCollection<string> AncestralWeapons { get; }
        public IReadOnlyCollection<IHouse> CadetBranches { get; }
        public IReadOnlyCollection<ICharacter> SwornMembers { get; }

        public HouseEntityAdapter(HouseEntity houseEntity)
        {
            if (houseEntity == null) throw new ArgumentNullException(nameof(houseEntity));
            _houseEntity = houseEntity;
        }
    }
}