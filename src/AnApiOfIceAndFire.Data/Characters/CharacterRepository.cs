using System;
using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterRepository : IEntityRepository<CharacterEntity, CharacterFilter>
    {
        private readonly string _connectionString;

        public CharacterRepository(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public Task<CharacterEntity> GetEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IPagedList<CharacterEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, CharacterFilter filter = null)
        {
            throw new System.NotImplementedException();
        }
    }
}