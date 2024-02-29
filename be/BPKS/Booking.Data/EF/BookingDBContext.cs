using Microsoft.EntityFrameworkCore;
using Booking.Data.Entities;
using Booking.Data.Configurations;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Booking.Data.EF
{
    public class BookingDbContext : IdentityDbContext<Account, Role, Guid>
    {
        //public BookingDbContext()
        //{
        //}

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<AppConfig> AppConfigs { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public virtual DbSet<ListParty> ListParties { get; set; }

        public virtual DbSet<ListProduct> ListProducts { get; set; }

        public virtual DbSet<ListRoom> ListRooms { get; set; }

        public virtual DbSet<Party> Parties { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        //    protected override void onconfiguring(dbcontextoptionsbuilder optionsbuilder)
        //#warning to protect potentially sensitive information in your connection string, you should move it out of source code. you can avoid scaffolding the connection string by using the name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. for more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?linkid=723263.
        //        => optionsbuilder.usesqlserver("server=(local);database=bkps;uid=sa;pwd=12345;trustservercertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());


            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "hometitle", Value = "this is home page of bookingsolution" },
                new AppConfig() { Key = "homekeyword", Value = "this is keyword of bookingsolution" },
                new AppConfig() { Key = "homedescription", Value = "this is description of bookingsolution" }
                );


            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

            });

            modelBuilder.Entity<AppConfig>(entity =>
            {
                entity.HasKey(e => e.Key).HasName("PK__AppConfi__C41E028839D1676F");

                entity.ToTable("AppConfig");

                entity.Property(e => e.Key).HasMaxLength(50);
                entity.Property(e => e.Value).HasMaxLength(500);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FeedBackId).HasName("PK__Feedback__E2CB3B87B174E4E6");

                entity.ToTable("Feedback");

                entity.Property(e => e.Feedback1)
                    .HasMaxLength(2000)
                    .HasColumnName("Feedback");

                entity.HasOne(d => d.Party).WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__Feedback__PartyI__3B75D760");
            });

            modelBuilder.Entity<ListParty>(entity =>
            {
                entity.HasKey(e => e.ListPartyId).HasName("PK__ListPart__CD4E0484680C6096");

                entity.ToTable("ListParty");

                entity.Property(e => e.ListPartyStatus).HasMaxLength(500);

                entity.HasOne(d => d.Party).WithMany(p => p.ListParties)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__ListParty__Party__35BCFE0A");
            });

            modelBuilder.Entity<ListProduct>(entity =>
            {
                entity.HasKey(e => e.ListProductId).HasName("PK__ListProd__BB3E314FADB6C270");

                entity.ToTable("ListProduct");

                entity.Property(e => e.ListProductStatus).HasMaxLength(500);

                entity.HasOne(d => d.Party).WithMany(p => p.ListProducts)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__ListProdu__Party__36B12243");

                entity.HasOne(d => d.Product).WithMany(p => p.ListProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ListProdu__Produ__38996AB5");

                entity.HasOne(d => d.Room).WithMany(p => p.ListProducts)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__ListProdu__RoomI__37A5467C");
            });

            modelBuilder.Entity<ListRoom>(entity =>
            {
                entity.HasKey(e => e.ListRoomId).HasName("PK__ListRoom__226F7FF07AF232EF");

                entity.ToTable("ListRoom");

                entity.Property(e => e.ListRoomStatus).HasMaxLength(500);

                entity.HasOne(d => d.Party).WithMany(p => p.ListRooms)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__ListRoom__PartyI__398D8EEE");

                entity.HasOne(d => d.Room).WithMany(p => p.ListRooms)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__ListRoom__RoomId__3A81B327");
            });

            modelBuilder.Entity<Party>(entity =>
            {
                entity.HasKey(e => e.PartyId).HasName("PK__Party__1640CD3337288F2D");

                entity.ToTable("Party");

                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.PartyName).HasMaxLength(255);
                entity.Property(e => e.PartyStatus).HasMaxLength(500);
                entity.Property(e => e.PhoneContact).HasMaxLength(50);
                entity.Property(e => e.Place).HasMaxLength(255);
                entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDD91D78CF");

                entity.ToTable("Product");

                entity.Property(e => e.ProductName).HasMaxLength(255);
                entity.Property(e => e.ProductStatus).HasMaxLength(500);
                entity.Property(e => e.ProductStyle).HasMaxLength(100);
                entity.Property(e => e.ProductType).HasMaxLength(50);
                entity.Property(e => e.ProductUrl).HasMaxLength(1000);
            });

            modelBuilder.Entity<Role>(entity =>
            {
               

                entity.ToTable("Roles");

                
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId).HasName("PK__Room__32863939AF1B1178");

                entity.ToTable("Room");

                entity.Property(e => e.RoomName).HasMaxLength(255);
                entity.Property(e => e.RoomStatus).HasMaxLength(500);
                entity.Property(e => e.RoomType).HasMaxLength(50);
                entity.Property(e => e.RoomUrl).HasMaxLength(1000);
            });

            modelBuilder.Entity<IdentityUser>().ToTable("Account").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<Account>().ToTable("Account").Property(p => p.Id).HasColumnName("Id");
         
          
            
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity <IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new {x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);
            //modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AspNetUserClaims");
            // modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AspNetUserClaims").HasKey(x => x.UserId);
  

            base.OnModelCreating(modelBuilder);

        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
