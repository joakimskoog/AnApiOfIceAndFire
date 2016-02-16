using System;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using Geymsla;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class CharacterService : BaseService<ICharacter, CharacterEntity, CharacterFilter>
    {
        private static readonly Expression<Func<CharacterEntity, object>>[] CharacterIncludeProperties = {
            character => character.Books,
            character => character.PovBooks,
            character => character.Father,
            character => character.Mother,
            character => character.Spouse,
            character => character.Allegiances
        };

        public CharacterService(IReadOnlyRepository<CharacterEntity,int> repository) : base(repository, CharacterIncludeProperties) { }

        protected override ICharacter CreateModel(CharacterEntity entity)
        {
            return new CharacterEntityAdapter(entity);
        }

        protected override Func<IQueryable<CharacterEntity>, IQueryable<CharacterEntity>> CreatePredicate(CharacterFilter filter)
        {
            Func<IQueryable<CharacterEntity>, IQueryable<CharacterEntity>> charaterFilters = characterEntities =>
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    characterEntities = characterEntities.Where(x => x.Name.Equals(filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.Culture))
                {
                    characterEntities = characterEntities.Where(x => x.Culture.Equals(filter.Culture));
                }
                if (!string.IsNullOrEmpty(filter.Born))
                {
                    characterEntities = characterEntities.Where(x => x.Born.Equals(filter.Born));
                }
                if (!string.IsNullOrEmpty(filter.Died))
                {
                    characterEntities = characterEntities.Where(x => x.Died.Equals(filter.Died));
                }
                if (filter.IsAlive.HasValue)
                {
                    characterEntities = characterEntities.Where(x => x.Died.Equals(string.Empty));
                }

                return characterEntities;
            };

            return charaterFilters;
        }
    }
}