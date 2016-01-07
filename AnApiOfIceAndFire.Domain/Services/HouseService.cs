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
    public class HouseService : BaseService<IHouse, HouseEntity>
    {
        public HouseService(IRepositoryWithIntKey<HouseEntity> repository) : base(repository, new Expression<Func<HouseEntity, object>>[0])
        {
        }

        protected override IHouse CreateModel(HouseEntity entity)
        {
            return new HouseEntityAdapter(entity);
        }
    }
}