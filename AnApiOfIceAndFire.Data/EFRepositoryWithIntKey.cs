using EntityFrameworkRepository;
using EntityFrameworkRepository.Entities;
// ReSharper disable InconsistentNaming

namespace AnApiOfIceAndFire.Data
{
    /// <summary>
    /// Convenience repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepositoryWithIntKey<TEntity> : EFRepository<TEntity, int>, IRepositoryWithIntKey<TEntity> where TEntity : class, IEntity<int>
    {
        public EFRepositoryWithIntKey(IEntityDbContext dbContext) : base(dbContext) { }
    }
}