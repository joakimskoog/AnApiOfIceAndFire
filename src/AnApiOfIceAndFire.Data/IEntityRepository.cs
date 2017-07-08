using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePagedList;

namespace AnApiOfIceAndFire.Data
{
    public interface IEntityRepository<TEntity, in TEntityFilter> where TEntity : BaseEntity where TEntityFilter : class
    {
        Task<TEntity> GetEntityAsync(int id);

        Task<IPagedList<TEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, TEntityFilter filter = null);

        Task InsertEntitiesAsync(List<TEntity> entities);
    }
}