using System.Data.Entity.ModelConfiguration;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Data.Configurations
{
    public class HouseConfiguration : EntityTypeConfiguration<HouseEntity>
    {
        public HouseConfiguration()
        {
            HasOptional(h => h.Founder)
                .WithMany()
                .HasForeignKey(h => h.FounderId);

            HasOptional(h => h.CurrentLord)
               .WithMany()
               .HasForeignKey(h => h.CurrentLordId);

            HasOptional(h => h.Heir)
               .WithMany()
               .HasForeignKey(h => h.HeirId);

            HasOptional(h => h.Overlord)
               .WithMany()
               .HasForeignKey(h => h.OverlordId);

            HasMany(h => h.CadetBranches)
                .WithMany()
                .Map(hh =>
                {
                    hh.MapLeftKey("HouseId1");
                    hh.MapRightKey("HouseId2");
                    hh.ToTable("HouseCadetBranches");
                });
        }
    }
}