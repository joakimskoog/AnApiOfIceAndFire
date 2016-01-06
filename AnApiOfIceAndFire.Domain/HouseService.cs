using System.Linq;
using AnApiOfIceAndFire.Domain.Models;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain
{
    public class HouseService : IModelService<IHouse>
    {
        public IHouse Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<IHouse> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<IHouse> GetPaginated(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }
    }
}