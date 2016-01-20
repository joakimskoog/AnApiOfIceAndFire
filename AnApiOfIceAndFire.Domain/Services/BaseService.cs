using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using Geymsla;
using Geymsla.Collections;

namespace AnApiOfIceAndFire.Domain.Services
{
    public abstract class BaseService<TModel, TEntity> : IModelService<TModel>
        where TEntity : BaseEntity
        where TModel : class
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

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync(CancellationToken.None, _includeProperties);
            if (entities == null)
            {
                return Enumerable.Empty<TModel>();
            }

            return entities.Select(CreateModel);
        }

        public async Task<IPagedList<TModel>> GetPaginatedAsync(int page, int pageSize)
        {
            var pagedEntities = await _repository.GetPaginatedListAsync(e => e.OrderBy(x => x.Id), page, pageSize, CancellationToken.None, _includeProperties);
            if (pagedEntities == null)
            {
                return new PagedList<TModel>(Enumerable.Empty<TModel>().AsQueryable(), page, pageSize);
            }

            return pagedEntities.ConvertTo(CreateModel);
        }

        protected abstract TModel CreateModel(TEntity entity);
    }
}