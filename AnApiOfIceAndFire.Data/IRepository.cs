using System;
using System.Linq;
using System.Linq.Expressions;

namespace AnApiOfIceAndFire.Data
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetSingleById(int id)
    }
}