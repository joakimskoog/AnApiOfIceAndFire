using EntityFrameworkRepository.Entities;

namespace AnApiOfIceAndFire.Data.Entities
{
    public abstract class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}