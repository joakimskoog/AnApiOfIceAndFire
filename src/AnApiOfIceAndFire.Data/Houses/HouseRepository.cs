using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseRepository : BaseRepository<HouseEntity, HouseFilter>
    {
        private const string SelectSingleHouseQuery = @"SELECT* FROM dbo.houses WHERE Id = @Id
                                                        SELECT* FROM dbo.character_house_link WHERE HouseId = @Id
                                                        SELECT* FROM dbo.house_cadetbranch_link WHERE HouseId = @Id";

        public HouseRepository(IOptions<ConnectionOptions> options) : base(options)
        {
        }

        public override async Task<HouseEntity> GetEntityAsync(int id)
        {
            using (var connection = new SqlConnection(Options.ConnectionString))
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

        public override async Task<IPagedList<HouseEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, HouseFilter filter)
        {
            var builder = new SqlBuilder();
            var countBuilder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT* FROM dbo.houses /**where**/ ORDER BY ID OFFSET @RowsToSkip ROWS FETCH NEXT @PageSize ROWS ONLY");
            var countTemplate = countBuilder.AddTemplate("SELECT COUNT(Id) FROM dbo.houses /**where**/");

            if (!string.IsNullOrEmpty(filter.Name))
            {
                builder.Where("Name = @Name", new { filter.Name });
                countBuilder.Where("Name = @Name", new { filter.Name });
            }
            if (!string.IsNullOrEmpty(filter.Region))
            {
                builder.Where("Region = @Region", new { filter.Region });
                countBuilder.Where("Region = @Region", new { filter.Region });
            }
            if (!string.IsNullOrEmpty(filter.Words))
            {
                builder.Where("Words = @Words", new { filter.Words });
                countBuilder.Where("Words = @Words", new { filter.Words });
            }
            if (filter.HasWords.HasValue)
            {
                if (filter.HasWords.Value)
                {
                    builder.Where("Words <> ''");
                    countBuilder.Where("Words <> ''");
                }
                else
                {
                    builder.Where("Words = ''");
                    countBuilder.Where("Words = ''");
                }
            }
            if (filter.HasTitles.HasValue)
            {
                if (filter.HasTitles.Value)
                {
                    builder.Where("Titles <> ''");
                    countBuilder.Where("Titles <> ''");
                }
                else
                {
                    builder.Where("Titles = ''");
                    countBuilder.Where("Titles = ''");
                }
            }
            if (filter.HasSeats.HasValue)
            {
                if (filter.HasSeats.Value)
                {
                    builder.Where("Seats <> ''");
                    countBuilder.Where("Seats <> ''");
                }
                else
                {
                    builder.Where("Seats = ''");
                    countBuilder.Where("Seats = ''");
                }
            }
            if (filter.HasDiedOut.HasValue)
            {
                if (filter.HasDiedOut.Value)
                {
                    builder.Where("DiedOut <> ''");
                    countBuilder.Where("DiedOut <> ''");
                }
                else
                {
                    builder.Where("DiedOut = ''");
                    countBuilder.Where("DiedOut = ''");
                }
            }
            if (filter.HasAncestralWeapons.HasValue)
            {
                if (filter.HasAncestralWeapons.Value)
                {
                    builder.Where("AncestralWeapons <> ''");
                    countBuilder.Where("AncestralWeapons <> ''");
                }
                else
                {
                    builder.Where("AncestralWeapons = ''");
                    countBuilder.Where("AncestralWeapons = ''");
                }
            }


            var rowsToSkip = (page - 1) * pageSize;
            builder.AddParameters(new { RowsToSkip = rowsToSkip, PageSize = pageSize });

            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                await connection.OpenAsync();
                var countTask = connection.QuerySingleAsync<int>(countTemplate.RawSql, countTemplate.Parameters);
                var houses = (await connection.QueryAsync<HouseEntity>(template.RawSql, template.Parameters)).ToList();

                var identifiers = houses.Select(h => h.Id).ToList();

                var relationshipsSql = @"SELECT* FROM dbo.character_house_link WHERE HouseId IN @hIdentifiers
                                         SELECT* FROM dbo.house_cadetbranch_link WHERE HouseId IN @hcbIdentifiers";
                
                using (var reader = await connection.QueryMultipleAsync(relationshipsSql, new { hIdentifiers = identifiers, hcbIdentifiers = identifiers }))
                {
                    var characterHouseRelationships = (await reader.ReadAsync()).GroupBy(hcr => hcr.HouseId).ToList();
                    var cadetBranchRelationships = (await reader.ReadAsync()).GroupBy(cbr => cbr.HouseId).ToList();

                    foreach (var house in houses)
                    {
                        var swornMembers = characterHouseRelationships.FirstOrDefault(hcr => hcr.Key == house.Id);
                        var cadetBranches = cadetBranchRelationships.FirstOrDefault(cbr => cbr.Key == house.Id);


                        if (swornMembers != null)
                        {
                            foreach (var member in swornMembers)
                            {
                                house.SwornMemberIdentifiers.Add(member.CharacterId);
                            }
                        }
                        if (cadetBranches != null)
                        {
                            foreach (var cadetBranch in cadetBranches)
                            {
                                house.CadetBranchIdentifiers.Add(cadetBranch.CadetBranchHouseId);
                            }
                        }
                    }
                }

                var totalNumberOfHouses = await countTask;

                return new PagedList<HouseEntity>(new PageMetadata(totalNumberOfHouses, page, pageSize), houses);
            }
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