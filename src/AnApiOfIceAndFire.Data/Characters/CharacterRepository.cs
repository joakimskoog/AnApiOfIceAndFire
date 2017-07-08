using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                using (var reader = await connection.QueryMultipleAsync(SelectSingleCharacterQuery, new {Id = id}))
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

        public override Task<IPagedList<CharacterEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, CharacterFilter filter = null)
        {
            throw new NotImplementedException();
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