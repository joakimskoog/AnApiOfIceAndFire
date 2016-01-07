using System.Collections.Generic;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain.Services
{
    public interface IModelService<T>
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        IPagedList<T> GetPaginated(int page, int pageSize);
    }
}