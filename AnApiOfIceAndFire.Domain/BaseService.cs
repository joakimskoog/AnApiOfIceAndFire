using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using Geymsla;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Domain
{
    public abstract class BaseService<TModel, TEntity, TFilter> : IModelService<TModel,TFilter>
        where TEntity : BaseEntity
        where TModel : class
        where TFilter : class
    {
        private readonly IReadOnlyRepository<TEntity, int> _repository;
        private readonly Expression<Func<TEntity, object>>[] _includeProperties;

        protected BaseService(IReadOnlyRepository<TEntity, int> repository, Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (includeProperties == null) throw new ArgumentNullException(nameof(includeProperties));
            _repository = repository;
            _includeProperties = includeProperties;
        }

        public async Task<TModel> GetAsync(int id)
        {
            var entity = await _repository.GetSingleOrDefaultAsync(x => x.Id == id, _includeProperties);
            return entity != null ? CreateModel(entity) : null;
        }

        public async Task<IPagedList<TModel>> GetPaginatedAsync(int page, int pageSize, TFilter filter = null)
        {
            var predicate = CreatePredicate(filter);
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderedPredicate = entities =>
            {
                var filteredEntities = predicate(entities);
                return filteredEntities.OrderBy(x => x.Id);
            };
           
            var pagedEntities = await _repository.GetPaginatedListAsync(orderedPredicate, page, pageSize, _includeProperties);
 
            if (pagedEntities == null)
            {
                return new PagedList<TModel>(Enumerable.Empty<TModel>().AsQueryable(), page, pageSize);
            }

            return pagedEntities.ConvertTo(CreateModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract TModel CreateModel(TEntity entity);

        /// <summary>
        /// Creates a function that tests each element for a condition.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected abstract Func<IQueryable<TEntity>, IQueryable<TEntity>> CreatePredicate(TFilter filter);
    }
}