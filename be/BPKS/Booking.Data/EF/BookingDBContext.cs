using Booking.Data.Configurations;
using Booking.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.EF
{
    public class BookingDBContext : DbContext
    {
        public BookingDBContext(DbContextOptions options) : base(options)
        {
            //options.UseSqlServer
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            //Cart
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            //Category(Party)
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //Order
            modelBuilder.ApplyConfiguration(new ParentConfiguration());

            //Data seeding
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "hometitle", Value = "this is home page of bookingsolution" },
                new AppConfig() { Key = "homekeyword", Value = "this is keyword of bookingsolution" },
                new AppConfig() { Key = "homedescription", Value = "this is description of bookingsolution" }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ListProduct> ListProducts { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Parent> Parents { get; set; }
    }
}
