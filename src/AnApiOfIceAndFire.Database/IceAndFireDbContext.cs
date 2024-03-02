//using AnApiOfIceAndFire.Database.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//namespace AnApiOfIceAndFire.Database
//{
//    public class IceAndFireDbContext : DbContext
//    {
//        public DbSet<Book> Books { get; set; }
//        public DbSet<Character> Characters { get; set; }
//        public DbSet<House> Houses { get; set; }

//        public IceAndFireDbContext(DbContextOptions<IceAndFireDbContext> options) 
//            : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            var stringToListValueConverter = new ValueConverter<List<string>, string>(
//                valueArr => string.Join(";", valueArr),
//                valueStr => valueStr.Split(";", StringSplitOptions.None).ToList());

//            modelBuilder
//                .Entity<Book>()
//                .Property(b => b.Authors)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<Book>()
//                .Property(b => b.MediaType)
//                .HasConversion<string>();

//            modelBuilder.Entity<Book>()
//                .HasMany(b => b.Characters)
//                .WithMany(c => c.Books)
//                .UsingEntity("BookCharacters");

//            modelBuilder.Entity<Book>()
//                .HasMany(b => b.PovCharacters)
//                .WithMany(b => b.PovBooks)
//                .UsingEntity("BookPovCharacters");

//            modelBuilder.Entity<Character>()
//                .Property(c => c.Aliases)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<Character>()
//                .Property(c => c.Titles)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<Character>()
//                .Property(c => c.TvSeries)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<Character>()
//                .Property(c => c.PlayedBy)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<Character>()
//                .HasOne(c => c.Father)
//                .WithOne()
//                .IsRequired(false);

//            modelBuilder.Entity<Character>()
//               .HasOne(c => c.Mother)
//               .WithOne()
//               .IsRequired(false);

//            modelBuilder.Entity<Character>()
//               .HasOne(c => c.Spouse)
//               .WithOne()
//               .IsRequired(false);

//            modelBuilder.Entity<Character>()
//                .HasOne(c => c.Founded)
//                .WithOne(h => h.Founder);

//            modelBuilder.Entity<Character>()
//                .HasOne(c => c.CurrentLordOver)
//                .WithOne(h => h.CurrentLord);

//            modelBuilder.Entity<Character>()
//                .HasOne(c => c.HeirTo)
//                .WithOne(h => h.Heir);

//            modelBuilder.Entity<House>()
//                .Property(h => h.Seats)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<House>()
//                .Property(h => h.Titles)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<House>()
//                .Property(h => h.AncestralWeapons)
//                .HasConversion(stringToListValueConverter);

//            modelBuilder.Entity<House>()
//                .HasMany(h => h.Characters)
//                .WithMany(c => c.Houses)
//                .UsingEntity("HouseCharacters");

//            modelBuilder.Entity<House>()
//                .HasMany(h => h.CadetBranches)
//                .WithOne(h => h.MainBranch);
//        }
//    }
//}