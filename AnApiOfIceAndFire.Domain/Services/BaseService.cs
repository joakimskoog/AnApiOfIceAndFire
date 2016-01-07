using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data;
using EntityFrameworkRepository.Entities;
using SimplePagination;

namespace AnApiOfIceAndFire.Domain.Services
{
    public abstract class BaseService<TModel, TEntity> : IModelService<TModel>
        where TEntity : class, IEntity<int>
        where TModel : class
    {
        private readonly IRepositoryWithIntKey<TEntity> _repository;
        private readonly Expression<Func<TEntity, object>>[] _includeProperties;

        protected BaseService(IRepositoryWithIntKey<TEntity> repository, Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (includeProperties == null) throw new ArgumentNullException(nameof(includeProperties));
            _repository = repository;
            _includeProperties = includeProperties;
        }

        public TModel Get(int id)
        {
            var entity = _repository.GetById(id, _includeProperties);
            return entity == null ? null : CreateModel(entity);
        }

        public IEnumerable<TModel> GetAll()
        {
            var entities = _repository.GetAll(includeProperties: _includeProperties);
            if (entities == null)
            {
                return Enumerable.Empty<TModel>();
            }

            return entities.Select(e => CreateModel(e));
        }

        public IPagedList<TModel> GetPaginated(int page, int pageSize)
        {
            var entities = _repository.GetAll(includeProperties: _includeProperties);
            if (entities == null)
            {
                return new PagedList<TModel>(Enumerable.Empty<TModel>().AsQueryable(), page, pageSize);
            }

            var pagedList = new PagedList<TEntity>(entities, page, pageSize);

            return pagedList.ConvertTo(CreateModel);
        }

        protected abstract TModel CreateModel(TEntity entity);
    }
}