using Google.Api.Ads.Common.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Data.Configurations
{
    public class AppConfigConfiguration : IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.HasKey(x => x.OAuth2PrivateKey); // Đặt khóa chính là Id
            builder.Property(x => x.RetryCount).IsRequired(true);
        }
    }
}
