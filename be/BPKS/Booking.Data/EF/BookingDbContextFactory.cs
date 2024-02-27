﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Booking.Data.EF
{
    public class BookingDbContextFactory : IDesignTimeDbContextFactory<BookingDbContext>
    {
        public BookingDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BKPS");

            var optionsBuilder = new DbContextOptionsBuilder<BookingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BookingDbContext(optionsBuilder.Options);
        }
    }
}
