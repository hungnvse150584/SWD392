using BPKS.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Booking.Data.EF
{
    public class BookingContextFactory : IDesignTimeDbContextFactory<BookingDBContext>
    {
        public BookingDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BPKS");

            var optionBuider = new DbContextOptionsBuilder<BookingDBContext>();
            optionBuider.UseSqlServer(connectionString);

            return new BookingDBContext(optionBuider.Options);
        }
    }
}
