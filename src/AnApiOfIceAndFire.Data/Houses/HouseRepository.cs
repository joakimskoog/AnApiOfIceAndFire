using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseRepository : IEntityRepository<HouseEntity, HouseFilter>
    {
        public Task<HouseEntity> GetEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<HouseEntity> GetPaginatedEntitiesAsync(int page, int pageSize, HouseFilter filter = null)
        {
            throw new System.NotImplementedException();
        }
    }
}