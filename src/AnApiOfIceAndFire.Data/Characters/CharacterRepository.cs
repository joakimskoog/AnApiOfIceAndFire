using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data.Characters
{
    public class CharacterRepository : IEntityRepository<CharacterEntity, CharacterFilter>
    {
        public Task<CharacterEntity> GetEntityAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<CharacterEntity> GetPaginatedEntitiesAsync(int page, int pageSize, CharacterFilter filter = null)
        {
            throw new System.NotImplementedException();
        }
    }
}