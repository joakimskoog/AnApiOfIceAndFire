using System.Linq;
using AnApiOfIceAndFire.Domain.Models;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain
{
    public class CharacterService : IModelService<ICharacter>
    {
        public ICharacter Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<ICharacter> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<ICharacter> GetPaginated(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }
    }
}