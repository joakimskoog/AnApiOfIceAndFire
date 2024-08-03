using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Books;
using AnApiOfIceAndFire.Data.Houses;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterRepository : BaseRepository<CharacterModel, CharacterFilter>
    {
        private const string SelectSingleCharacterQuery = @"SELECT* FROM Characters WHERE Id = @Id;
                                                            SELECT HouseId FROM HouseCharacters WHERE CharacterId = @Id;
                                                            SELECT* FROM BookCharacters WHERE CharacterId = @Id;";

        public CharacterRepository(IOptions<ConnectionOptions> options) : base(options)
        {
        }

        public override async Task<CharacterModel> GetEntityAsync(int id)
        {
            using (var connection = new SqliteConnection(Options.AnApiOfIceAndFireDatabase))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleCharacterQuery, new { Id = id }))
                {
                    var character = await reader.ReadFirstOrDefaultAsync<CharacterModel>();

                    if (character != null)
                    {
                        foreach (var houseId in await reader.ReadAsync<int>())
                        {
                            character.AllegianceIdentifiers.Add(houseId);
                        }
                        foreach (var book in await reader.ReadAsync<BookCharacter>())
                        {
                            var bookId = book.BookId;
                            var type = book.Type;

                            if (type == 0)
                            {
                                character.BookIdentifiers.Add(bookId);
                            }
                            else
                            {
                                character.PovBookIdentifiers.Add(bookId);
                            }
                        }
                    }

                    return character;
                }
            }
        }

        public override async Task<IPagedList<CharacterModel>> GetPaginatedEntitiesAsync(int page, int pageSize, CharacterFilter filter)
        {
            var builder = new SqlBuilder();
            var countBuilder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT* FROM Characters /**where**/ ORDER BY ID LIMIT @PageSize OFFSET @RowsToSkip");
            var countTemplate = countBuilder.AddTemplate("SELECT COUNT(Id) FROM Characters /**where**/");

            if (!string.IsNullOrEmpty(filter.Name))
            {
                builder.Where("Name = @Name", new { filter.Name });
                countBuilder.Where("Name = @Name", new { filter.Name });
            }
            if (!string.IsNullOrEmpty(filter.Culture))
            {
                builder.Where("Culture = @Culture", new { filter.Culture });
                countBuilder.Where("Culture = @Culture", new { filter.Culture });
            }
            if (!string.IsNullOrEmpty(filter.Born))
            {
                builder.Where("Born = @Born", new { filter.Born });
                countBuilder.Where("Born = @Born", new { filter.Born });
            }
            if (!string.IsNullOrEmpty(filter.Died))
            {
                builder.Where("Died = @Died", new { filter.Died });
                countBuilder.Where("Died = @Died", new { filter.Died });
            }
            if (filter.IsAlive.HasValue)
            {
                if (filter.IsAlive.Value)
                {
                    builder.Where("Died IS NULL OR Died = ''");
                    countBuilder.Where("Died IS NULL OR Died = ''");
                }
                else
                {
                    builder.Where("Died IS NOT NULL AND Died <> ''");
                    countBuilder.Where("Died IS NOT NULL AND Died <> ''");
                }
            }
            if (filter.Gender.HasValue)
            {
                builder.Where("Gender = @Gender", new { @Gender = (int)filter.Gender.Value });
            }

            var rowsToSkip = (page - 1) * pageSize;
            builder.AddParameters(new { RowsToSkip = rowsToSkip, PageSize = pageSize });

            using (var connection = new SqliteConnection(Options.AnApiOfIceAndFireDatabase))
            {
                await connection.OpenAsync();
                var countTask = connection.QuerySingleAsync<int>(countTemplate.RawSql, countTemplate.Parameters);
                var characters = (await connection.QueryAsync<CharacterModel>(template.RawSql, template.Parameters)).ToList();

                var identifiers = characters.Select(c => c.Id).ToList();

                var relationshipsSql = @"SELECT* FROM BookCharacters WHERE CharacterId IN @bIdentifiers;
                                         SELECT* FROM HouseCharacters WHERE CharacterId IN @hIdentifiers;";

                using (var reader = await connection.QueryMultipleAsync(relationshipsSql, new { bIdentifiers = identifiers, hIdentifiers = identifiers }))
                {
                    var charaterBookRelationships = (await reader.ReadAsync<BookCharacter>()).GroupBy(cbr => cbr.CharacterId).ToList();
                    var characterHouseRelationships = (await reader.ReadAsync<HouseCharacter>()).GroupBy(chr => chr.CharacterId).ToList();

                    foreach (var character in characters)
                    {
                        var books = charaterBookRelationships.FirstOrDefault(cbr => cbr.Key == character.Id);
                        var allegiances = characterHouseRelationships.FirstOrDefault(chr => chr.Key == character.Id);

                        if (books != null)
                        {
                            foreach (var book in books)
                            {
                                var type = book.Type;
                                if (type == 0)
                                {
                                    character.BookIdentifiers.Add(book.BookId);
                                }
                                else
                                {
                                    character.PovBookIdentifiers.Add(book.BookId);
                                }
                            }
                        }

                        if (allegiances != null)
                        {
                            foreach (var allegiance in allegiances)
                            {
                                character.AllegianceIdentifiers.Add(allegiance.HouseId);
                            }
                        }
                    }
                }

                var totalNumberOfCharacters = await countTask;

                return new PagedList<CharacterModel>(new PageMetadata(totalNumberOfCharacters, page, pageSize), characters);
            }
        }
    }
}