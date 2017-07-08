using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterRepository : BaseRepository<CharacterEntity, CharacterFilter>
    {
        public CharacterRepository(string connectionString) : base(connectionString)
        {
        }

        public override Task<CharacterEntity> GetEntityAsync(int id)
        {
            throw new NotImplementedException();
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