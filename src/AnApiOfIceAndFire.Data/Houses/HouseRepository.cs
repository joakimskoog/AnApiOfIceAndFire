using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Houses
{
    public class HouseRepository : BaseRepository<HouseModel, HouseFilter>
    {
        private const string SelectSingleHouseQuery = @"SELECT* FROM Houses WHERE Id = @Id;
                                                        SELECT CharacterId FROM HouseCharacters WHERE HouseId = @Id;
                                                        SELECT Id FROM Houses WHERE MainBranchId = @Id;";

        public HouseRepository(IOptions<ConnectionOptions> options) : base(options)
        {
        }

        public override async Task<HouseModel> GetEntityAsync(int id)
        {
            using (var connection = new SqliteConnection(Options.AnApiOfIceAndFireDatabase))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleHouseQuery, new { Id = id }))
                {
                    var house = await reader.ReadFirstOrDefaultAsync<HouseModel>();

                    if (house != null)
                    {
                        foreach (var swornMemberId in await reader.ReadAsync<int>())
                        {
                            //var memberId = swornMember.CharacterId;
                            house.SwornMemberIdentifiers.Add(swornMemberId);
                        }
                        foreach (var cadetBranchId in await reader.ReadAsync<int>())
                        {
                            //var cadetBranchId = cadetBranch.CadetBranchHouseId;
                            house.CadetBranchIdentifiers.Add(cadetBranchId);
                        }
                    }

                    return house;
                }
            }
        }

        public override async Task<IPagedList<HouseModel>> GetPaginatedEntitiesAsync(int page, int pageSize, HouseFilter filter)
        {
            var builder = new SqlBuilder();
            var countBuilder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT* FROM Houses /**where**/ ORDER BY ID LIMIT @PageSize OFFSET @RowsToSkip");
            var countTemplate = countBuilder.AddTemplate("SELECT COUNT(Id) FROM Houses /**where**/");

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

            using (var connection = new SqliteConnection(Options.AnApiOfIceAndFireDatabase))
            {
                await connection.OpenAsync();
                var countTask = connection.QuerySingleAsync<int>(countTemplate.RawSql, countTemplate.Parameters);
                var houses = (await connection.QueryAsync<HouseModel>(template.RawSql, template.Parameters)).ToList();

                var identifiers = houses.Select(h => h.Id).ToList();

                var relationshipsSql = @"SELECT* FROM HouseCharacters WHERE HouseId IN @hIdentifiers;
                                         SELECT Id as CadetBranchId, MainBranchId FROM Houses WHERE MainBranchId IN @hcbIdentifiers;";
                
                using (var reader = await connection.QueryMultipleAsync(relationshipsSql, new { hIdentifiers = identifiers, hcbIdentifiers = identifiers }))
                {
                    var characterHouseRelationships = (await reader.ReadAsync<HouseCharacter>()).GroupBy(hcr => hcr.HouseId).ToList();
                    var cadetBranchRelationships = (await reader.ReadAsync<HouseCadetBranch>()).GroupBy(cbr => cbr.MainBranchId).ToList();

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
                                house.CadetBranchIdentifiers.Add(cadetBranch.CadetBranchId);
                            }
                        }
                    }
                }

                var totalNumberOfHouses = await countTask;

                return new PagedList<HouseModel>(new PageMetadata(totalNumberOfHouses, page, pageSize), houses);
            }
        }
    }
}