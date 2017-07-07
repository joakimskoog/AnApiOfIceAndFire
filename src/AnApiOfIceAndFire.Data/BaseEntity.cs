namespace AnApiOfIceAndFire.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
    }
}