using System.Linq;
using AnApiOfIceAndFire.Domain.Models;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain
{
    public class BookService : IModelService<IBook>
    {
        public IBook Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<IBook> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IPagedList<IBook> GetPaginated(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }
    }
}