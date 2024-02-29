using Google.Api.Ads.Common.Lib;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Data.Entities;

namespace Booking.Data.Configurations
{
    internal class AspNetUsersConfiguration : IEntityTypeConfiguration<AspNetUsers>
    {
        public void Configure(EntityTypeBuilder<AspNetUsers> builder)
        {
            builder.ToTable("AspNetUsers");
        }
    }
}
