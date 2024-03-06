using System;
using System.Threading.Tasks;
using SimplePagedList;
using Microsoft.Extensions.Options;

namespace AnApiOfIceAndFire.Data
{
    public abstract class BaseRepository<TEntity, TEntityFilter> : IEntityRepository<TEntity, TEntityFilter> where TEntityFilter : class where TEntity : BaseModel
    {
        protected readonly ConnectionOptions Options;


        protected BaseRepository(IOptions<ConnectionOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            Options = options.Value;
        }

        public abstract Task<TEntity> GetEntityAsync(int id);

        public abstract Task<IPagedList<TEntity>> GetPaginatedEntitiesAsync(int page, int pageSize, TEntityFilter filter);
    }

    public class ConnectionOptions
    {
        public string AnApiOfIceAndFireDatabase { get; set; }
    }
}