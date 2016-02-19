using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Adapters;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Domain.Models.Filters;
using Geymsla;

namespace AnApiOfIceAndFire.Domain.Services
{
    public class HouseService : BaseService<IHouse, HouseEntity, HouseFilter>
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

        protected override Func<IQueryable<HouseEntity>, IQueryable<HouseEntity>> CreatePredicate(HouseFilter filter)
        {
            Func<IQueryable<HouseEntity>, IQueryable<HouseEntity>> houseFilters = houseEntities =>
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    houseEntities = houseEntities.Where(x => x.Name.Equals(filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.Region))
                {
                    houseEntities = houseEntities.Where(x => x.Region.Equals(filter.Region));
                }
                if (!string.IsNullOrEmpty(filter.Words))
                {
                    houseEntities = houseEntities.Where(x => x.Words.Equals(filter.Words));
                }
                if (filter.HasWords.HasValue)
                {
                    houseEntities = houseEntities.Where(x => x.Words.Length > 0);
                }
                if (filter.HasTitles.HasValue)
                {
                    houseEntities = houseEntities.Where(x => !string.IsNullOrEmpty(x.TitlesRaw));
                }
                if (filter.HasSeats.HasValue)
                {
                    houseEntities = houseEntities.Where(x => !string.IsNullOrEmpty(x.SeatsRaw));
                }
                if (filter.HasDiedOut.HasValue)
                {
                    houseEntities = houseEntities.Where(x => x.DiedOut.Length > 0);
                }
                if (filter.HasAncestralWeapons.HasValue)
                {
                    houseEntities = houseEntities.Where(x => !string.IsNullOrEmpty(x.AncestralWeaponsRaw));
                }

                return houseEntities;
            };

            return houseFilters;
        }
    }
}