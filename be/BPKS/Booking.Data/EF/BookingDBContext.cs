using Booking.Data.Configurations;
using Booking.Data.Enities;
using Google.Api.Ads.Common.Lib;
using Microsoft.EntityFrameworkCore;

namespace BPKS.EF
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

            //Data seeding
            //modelBuilder.Entity<AppConfig>().HasData(
            //    new AppConfig() { Key = "HomeTitle", Value = "This is home page of BookingSolution" },
            //    new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of BookingSolution" },
            //    new AppConfig() { Key = "HomeDescription", Value = "This is description of BookingSolution" }
            //    );
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Party> Parties { get; set; }
    }
}
