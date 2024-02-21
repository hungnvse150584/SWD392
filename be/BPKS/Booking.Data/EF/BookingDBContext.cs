using Booking.Data.Configurations;
using BPKS.Entities;
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
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Parent> Parents { get; set; }
    }
}
