using System.Collections.Generic;
using System.Threading.Tasks;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Domain.Services
{
    public interface IModelService<T>
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IPagedList<T>> GetPaginatedAsync(int page, int pageSize);
    }
}