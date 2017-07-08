using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseRepository : BaseRepository<HouseEntity, HouseFilter>
    {
        public HouseRepository(string connectionString) : base(connectionString)
        {
        }

        public override Task<HouseEntity> GetEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<HouseEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, HouseFilter filter = null)
        {
            throw new NotImplementedException();
        }

        protected override async Task InsertRelationships(HouseEntity house, SqlTransaction transaction, SqlConnection connection)
        {
            var insertTasks = new List<Task>();

            foreach (var cadetBranch in house.CadetBranchIdentifiers)
            {
                var task = connection.ExecuteAsync("INSERT INTO dbo.house_cadetbranch_link VALUES(@HouseId, @CadetBranchHouseId)", new
                {
                    HouseId = house.Id,
                    CadetBranchHouseId = cadetBranch
                }, transaction);
                insertTasks.Add(task);
            }

            await Task.WhenAll(insertTasks);
        }
    }
}