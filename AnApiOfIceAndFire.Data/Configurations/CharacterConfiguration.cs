using System.Data.Entity.ModelConfiguration;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Data.Configurations
{
    public class CharacterConfiguration : EntityTypeConfiguration<CharacterEntity>
    {
        public CharacterConfiguration()
        {
            HasOptional(c => c.Father)
                .WithMany()
                .HasForeignKey(c => c.FatherId);

            HasOptional(c => c.Mother)
                .WithMany()
                .HasForeignKey(c => c.MotherId);

            HasOptional(c => c.Spouse)
                .WithMany()
                .HasForeignKey(c => c.SpouseId);


            HasMany(c => c.Books)
                .WithMany(b => b.Characters)
                .Map(cb =>
                {
                    cb.MapLeftKey("BookId");
                    cb.MapRightKey("CharacterId");
                    cb.ToTable("BookCharacters");
                });

            HasMany(c => c.PovBooks)
                .WithMany(b => b.PovCharacters)
                .Map(cb =>
                {
                    cb.MapLeftKey("BookId");
                    cb.MapRightKey("CharacterId");
                    cb.ToTable("PovBookCharacters");
                });

            HasMany(c => c.Allegiances)
                .WithMany(h => h.SwornMembers)
                .Map(x =>
                {
                    x.MapLeftKey("CharacterId");
                    x.MapRightKey("HouseId");
                    x.ToTable("CharacterHouseAllegiances");
                });
        }
    }
}