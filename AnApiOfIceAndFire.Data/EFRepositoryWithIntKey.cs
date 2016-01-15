using System;
using System.Linq;
using System.Linq.Expressions;
using AnApiOfIceAndFire.Data.Entities;
using EntityFrameworkRepository;

// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data
{
    /// <summary>
    /// Convenience repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepositoryWithIntKey<TEntity> : EFRepository<TEntity, int>, IRepositoryWithIntKey<TEntity> where TEntity : BaseEntity
    {
        public EFRepositoryWithIntKey(IEntityDbContext dbContext) : base(dbContext) { }

        public TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entity = GetAll(includeProperties: includeProperties).SingleOrDefault(x => x.Id == id);
            return entity;
        }
    }
}