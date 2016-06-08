using System;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using Geymsla;

namespace AnApiOfIceAndFire.Domain.Characters
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
                if (filter.Name != null)
                {
                    characterEntities = characterEntities.Where(x => x.Name.Equals(filter.Name));
                }
                if (filter.Culture != null)
                {
                    characterEntities = characterEntities.Where(x => x.Culture.Equals(filter.Culture));
                }
                if (filter.Born != null)
                {
                    characterEntities = characterEntities.Where(x => x.Born.Equals(filter.Born));
                }
                if (filter.Died != null)
                {
                    characterEntities = characterEntities.Where(x => x.Died.Equals(filter.Died));
                }
                if (filter.IsAlive.HasValue)
                {
                    characterEntities = characterEntities.Where(x => string.IsNullOrEmpty(x.Died));
                }
                if (filter.Gender.HasValue)
                {
                    if (filter.Gender.Value == Gender.Female)
                    {
                        characterEntities = characterEntities.Where(x => x.IsFemale.HasValue && x.IsFemale.Value);
                    }
                    else if (filter.Gender.Value == Gender.Male)
                    {
                        characterEntities = characterEntities.Where(x => x.IsFemale.HasValue && !x.IsFemale.Value);
                    }
                    else
                    {
                        characterEntities = characterEntities.Where(x => !x.IsFemale.HasValue);
                    }
                }

                return characterEntities;
            };

            return charaterFilters;
        }
    }
}