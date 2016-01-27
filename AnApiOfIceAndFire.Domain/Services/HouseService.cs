using System;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;
using Geymsla;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class HouseService : BaseService<IHouse, HouseEntity>
    {
        private static readonly Expression<Func<HouseEntity, object>>[] HouseIncludeProperties =
        {
            house => house.CurrentLord,
            house => house.Heir,
            house => house.Overlord,
            house => house.Founder,
            house => house.CadetBranches,
            house => house.SwornMembers
        };

        public HouseService(IReadOnlyRepository<HouseEntity,int> repository) : base(repository, HouseIncludeProperties) { }

        protected override IHouse CreateModel(HouseEntity entity)
        {
            return new HouseEntityAdapter(entity);
        }
    }
}