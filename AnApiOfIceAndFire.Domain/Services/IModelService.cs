using System.Threading.Tasks;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Domain.Services
{
    public interface IModelService<T, in TFilter> where TFilter : class
    {
        Task<T> GetAsync(int id);

        Task<IPagedList<T>> GetPaginatedAsync(int page, int pageSize, TFilter filter = null);
    }
}