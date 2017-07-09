using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterRepository : BaseRepository<CharacterEntity, CharacterFilter>
    {
        private const string SelectSingleCharacterQuery = @"SELECT* FROM dbo.characters WHERE Id = @Id
                                                            SELECT* FROM dbo.character_house_link WHERE CharacterId = @Id
                                                            SELECT* FROM dbo.book_character_link WHERE CharacterId = @Id";

        public CharacterRepository(IOptions<ConnectionOptions> options) : base(options)
        {
        }

        public override async Task<CharacterEntity> GetEntityAsync(int id)
        {
            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                using (var reader = await connection.QueryMultipleAsync(SelectSingleCharacterQuery, new { Id = id }))
                {
                    var character = await reader.ReadFirstOrDefaultAsync<CharacterEntity>();

                    if (character != null)
                    {
                        foreach (var allegiance in await reader.ReadAsync())
                        {
                            var houseId = allegiance.HouseId;
                            character.AllegianceIdentifiers.Add(houseId);
                        }
                        foreach (var book in await reader.ReadAsync())
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

        public override async Task<IPagedList<CharacterEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, CharacterFilter filter)
        {
            var builder = new SqlBuilder();
            var countBuilder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT* FROM dbo.characters /**where**/ ORDER BY ID OFFSET @RowsToSkip ROWS FETCH NEXT @PageSize ROWS ONLY");
            var countTemplate = countBuilder.AddTemplate("SELECT COUNT(Id) FROM dbo.characters /**where**/");

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
                    builder.Where("Died = @AliveDead", new { AliveDead = "''" });
                    countBuilder.Where("Died = @AliveDead", new { AliveDead = "''" });
                }
                else
                {
                    builder.Where("Died <> @DiedDead", new { DiedDead = "''" });
                    countBuilder.Where("Died <> @DiedDead", new { DiedDead = "''" });
                }
            }
            if (filter.Gender.HasValue)
            {
                if (filter.Gender.Value == Gender.Female)
                {
                    builder.Where("IsFemale = @IsFemale", new { IsFemale = 1 });
                    countBuilder.Where("IsFemale = @IsFemale", new { IsFemale = 1 });
                }
                else if (filter.Gender.Value == Gender.Male)
                {
                    builder.Where("IsFemale = @IsFemale", new { IsFemale = 0 });
                    countBuilder.Where("IsFemale = @IsFemale", new { IsFemale = 0 });
                }
            }

            var rowsToSkip = (page - 1) * pageSize;
            builder.AddParameters(new { RowsToSkip = rowsToSkip, PageSize = pageSize });

            using (var connection = new SqlConnection(Options.ConnectionString))
            {
                await connection.OpenAsync();
                var countTask = connection.QuerySingleAsync<int>(countTemplate.RawSql, countTemplate.Parameters);
                var characters = (await connection.QueryAsync<CharacterEntity>(template.RawSql, template.Parameters)).ToList();

                var identifiers = characters.Select(c => c.Id).ToList();

                var relationshipsSql = @"SELECT* FROM dbo.book_character_link WHERE CharacterId IN @bIdentifiers
                                         SELECT* FROM dbo.character_house_link WHERE CharacterId IN @hIdentifiers";

                using (var reader = await connection.QueryMultipleAsync(relationshipsSql, new { bIdentifiers = identifiers, hIdentifiers = identifiers }))
                {
                    var charaterBookRelationships = (await reader.ReadAsync()).GroupBy(cbr => cbr.CharacterId).ToList();
                    var characterHouseRelationships = (await reader.ReadAsync()).GroupBy(chr => chr.CharacterId).ToList();

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

                return new PagedList<CharacterEntity>(new PageMetadata(totalNumberOfCharacters, page, pageSize), characters);
            }
        }

        protected override async Task InsertRelationships(CharacterEntity character, SqlTransaction transaction, SqlConnection connection)
        {
            var insertTasks = new List<Task>();

            foreach (var book in character.BookIdentifiers)
            {
                var task = connection.ExecuteAsync("INSERT INTO dbo.book_character_link VALUES(@BookId, @CharacterId, @Type)", new
                {
                    BookId = book,
                    CharacterId = character.Id,
                    Type = 0
                }, transaction);
                insertTasks.Add(task);
            }

            foreach (var povBook in character.PovBookIdentifiers)
            {
                var task = connection.ExecuteAsync(
                "INSERT INTO dbo.book_character_link VALUES(@BookId, @CharacterId, @Type)", new
                {
                    BookId = povBook,
                    CharacterId = character.Id,
                    Type = 1
                }, transaction);
                insertTasks.Add(task);
            }

            foreach (var allegiance in character.AllegianceIdentifiers)
            {
                var task = connection.ExecuteAsync("INSERT INTO dbo.character_house_link VALUES(@CharacterId, @HouseId)", new
                {
                    CharacterId = character.Id,
                    HouseId = allegiance
                }, transaction);

                insertTasks.Add(task);
            }

            await Task.WhenAll(insertTasks);
        }
    }
}