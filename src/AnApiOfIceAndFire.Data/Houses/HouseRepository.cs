using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Characters;
using Dapper;
using Dapper.Contrib.Extensions;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseRepository : BaseRepository<HouseEntity, HouseFilter>
    {
        private const string SelectSingleHouseQuery = @"SELECT* FROM dbo.houses WHERE Id = @Id
                                                        SELECT* FROM dbo.character_house_link WHERE HouseId = @Id
                                                        SELECT* FROM dbo.house_cadetbranch_link WHERE HouseId = @Id";


        public HouseRepository(string connectionString) : base(connectionString)
        {
        }

        public override async Task<HouseEntity> GetEntityAsync(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleHouseQuery, new { Id = id }))
                {
                    var house = await reader.ReadFirstOrDefaultAsync<HouseEntity>();

                    if (house != null)
                    {
                        foreach (var swornMember in await reader.ReadAsync())
                        {
                            var memberId = swornMember.CharacterId;
                            house.SwornMemberIdentifiers.Add(memberId);
                        }
                        foreach (var cadetBranch in await reader.ReadAsync())
                        {
                            var cadetBranchId = cadetBranch.CadetBranchHouseId;
                            house.CadetBranchIdentifiers.Add(cadetBranchId);
                        }
                    }

                    return house;
                }
            }
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