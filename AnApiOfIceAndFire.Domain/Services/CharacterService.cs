using System;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class CharacterService : BaseService<ICharacter, CharacterEntity>
    {
        private static readonly Expression<Func<CharacterEntity, object>>[] CharacterIncludeProperties = {
            character => character.Books,
            character => character.PovBooks,
            character => character.Father,
            character => character.Mother,
            character => character.Spouse,
            character => character.Allegiances
        };

        public CharacterService(IRepositoryWithIntKey<CharacterEntity> repository) : base(repository, CharacterIncludeProperties) { }

        protected override ICharacter CreateModel(CharacterEntity entity)
        {
            return new CharacterEntityAdapter(entity);
        }
    }
}