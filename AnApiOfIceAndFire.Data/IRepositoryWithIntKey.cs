using System;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using EntityFrameworkRepository;

namespace AnApiOfIceAndFire.Data
{
    /// <summary>
    /// Convenience interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryWithIntKey<TEntity> : IRepository<TEntity, int> where TEntity : BaseEntity
    {
        TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}