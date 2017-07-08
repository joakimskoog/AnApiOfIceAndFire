using System;
using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseRepository : IEntityRepository<HouseEntity, HouseFilter>
    {
        private readonly string _connectionString;

        public HouseRepository(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public Task<HouseEntity> GetEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IPagedList<HouseEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, HouseFilter filter = null)
        {
            throw new System.NotImplementedException();
        }

    }
}