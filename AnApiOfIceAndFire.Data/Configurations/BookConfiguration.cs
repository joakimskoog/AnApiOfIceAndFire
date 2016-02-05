using System.Data.Entity.ModelConfiguration;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Data.Configurations
{
    public class BookConfiguration : EntityTypeConfiguration<BookEntity>
    {
        public BookConfiguration()
        {
            Ignore(b => b.Authors);

            HasMany(c => c.Characters)
                .WithMany(b => b.Books)
                .Map(cb =>
                {
                    cb.MapLeftKey("CharacterId");
                    cb.MapRightKey("BookId");
                    cb.ToTable("BookCharacters");
                });

            HasMany(c => c.PovCharacters)
                .WithMany(b => b.PovBooks)
                .Map(cb =>
                {
                    cb.MapLeftKey("CharacterId");
                    cb.MapRightKey("BookId");
                    cb.ToTable("PovBookCharacters");
                });
        }
    }
}