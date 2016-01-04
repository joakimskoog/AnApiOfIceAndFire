using System.Linq;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain
{
    public interface IModelService<T>
    {
        T Get(int id);

        IQueryable<T> GetAll();

        IPagedList<T> GetPaginated(int page, int pageSize);
    }
}