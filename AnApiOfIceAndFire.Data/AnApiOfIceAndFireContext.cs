using System.Data.Entity;
using AnApiOfIceAndFire.Data.Configurations;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Data
{
    public class AnApiOfIceAndFireContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<CharacterEntity> Characters { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }   

        public AnApiOfIceAndFireContext() : base("AnApiOfIceAndFire_db")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new CharacterConfiguration());
            modelBuilder.Configurations.Add(new HouseConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}