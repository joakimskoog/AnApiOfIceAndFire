using EntityFrameworkRepository;
using EntityFrameworkRepository.Entities;

namespace AnApiOfIceAndFire.Data
{
    /// <summary>
    /// Convenience interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryWithIntKey<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    { }
}