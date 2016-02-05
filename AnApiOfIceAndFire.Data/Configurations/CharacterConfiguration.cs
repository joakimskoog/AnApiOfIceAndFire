using System.Data.Entity.ModelConfiguration;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Data.Configurations
{
    public class CharacterConfiguration : EntityTypeConfiguration<CharacterEntity>
    {
        public CharacterConfiguration()
        {
            Ignore(c => c.Aliases)
                .Ignore(c => c.Titles)
                .Ignore(c => c.TvSeries)
                .Ignore(c => c.PlayedBy);

            HasOptional(c => c.Father)
                .WithMany()
                .HasForeignKey(c => c.FatherId);

            HasOptional(c => c.Mother)
                .WithMany()
                .HasForeignKey(c => c.MotherId);

            HasOptional(c => c.Spouse)
                .WithMany()
                .HasForeignKey(c => c.SpouseId);
       
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