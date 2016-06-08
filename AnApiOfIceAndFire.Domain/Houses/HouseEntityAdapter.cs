using System;
using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Characters;

namespace AnApiOfIceAndFire.Domain.Houses
{
    public class HouseEntityAdapter : IHouse
    {
        private readonly HouseEntity _houseEntity;

        public int Identifier => _houseEntity.Id;
        public string Name => _houseEntity.Name;
        public string Region => _houseEntity.Region;
        public string CoatOfArms => _houseEntity.CoatOfArms;
        public string Words => _houseEntity.Words;
        public IReadOnlyCollection<string> Titles => _houseEntity.Titles;
        public IReadOnlyCollection<string> Seats => _houseEntity.Seats;

        private ICharacter _currentLord;
        public ICharacter CurrentLord
        {
            get
            {
                if (_houseEntity.CurrentLord == null)
                {
                    return null;
                }

                return _currentLord ?? (_currentLord = new CharacterEntityAdapter(_houseEntity.CurrentLord));
            }
        }

        private ICharacter _heir;
        public ICharacter Heir
        {
            get
            {
                if (_houseEntity.Heir == null)
                {
                    return null;
                }

                return _heir ?? (_heir = new CharacterEntityAdapter(_houseEntity.Heir));
            }
        }

        private IHouse _overlord;
        public IHouse Overlord
        {
            get
            {
                if (_houseEntity.Overlord == null)
                {
                    return null;
                }

                return _overlord ?? (_overlord = new HouseEntityAdapter(_houseEntity.Overlord));
            }
        }
        public string Founded => _houseEntity.Founded;

        private ICharacter _founder;
        public ICharacter Founder
        {
            get
            {
                if (_houseEntity.Founder == null)
                {
                    return null;
                }

                return _founder ?? (_founder = new CharacterEntityAdapter(_houseEntity.Founder));
            }
        }
        public string DiedOut => _houseEntity.DiedOut;
        public IReadOnlyCollection<string> AncestralWeapons => _houseEntity.AncestralWeapons;

        private IReadOnlyCollection<IHouse> _cadetBranches;
        public IReadOnlyCollection<IHouse> CadetBranches
        {
            get
            {
                return _cadetBranches ?? (_cadetBranches = _houseEntity.CadetBranches.Select(h => new HouseEntityAdapter(h)).ToList());
            }
        }

        private IReadOnlyCollection<ICharacter> _swornMembers;
        public IReadOnlyCollection<ICharacter> SwornMembers
        {
            get
            {
                return _swornMembers ?? (_swornMembers = _houseEntity.SwornMembers.Select(c => new CharacterEntityAdapter(c)).ToList());
            }
        }

        public HouseEntityAdapter(HouseEntity houseEntity)
        {
            if (houseEntity == null) throw new ArgumentNullException(nameof(houseEntity));
            _houseEntity = houseEntity;
        }
    }
}