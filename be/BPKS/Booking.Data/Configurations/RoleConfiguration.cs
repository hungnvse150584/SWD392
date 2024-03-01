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
    public class AppNetRoleConfiguration : IEntityTypeConfiguration<AppNetRole>
    {
        public void Configure(EntityTypeBuilder<AppNetRole> builder)
        {
            builder.ToTable("AppNetRoles");

            
            

        }
    }
}
