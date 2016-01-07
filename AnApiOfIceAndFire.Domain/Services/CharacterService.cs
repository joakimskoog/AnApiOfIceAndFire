using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class CharacterService : BaseService<ICharacter,CharacterEntity>
    {
        public CharacterService(IRepositoryWithIntKey<CharacterEntity> repository) : base(repository, new Expression<Func<CharacterEntity, object>>[0])
        {
        }

        protected override ICharacter CreateModel(CharacterEntity entity)
        {
            return new CharacterEntityAdapter(entity);
        }
    }
}