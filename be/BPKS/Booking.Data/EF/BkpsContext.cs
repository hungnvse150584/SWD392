using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Booking.Data.Entities;
<<<<<<< Updated upstream
using Booking.Data.Enities;




namespace Booking.Data.EF;




public partial class BkpsContext : DbContext
{
    public BkpsContext()
=======
namespace Booking.Data.EF {
    public partial class BkpsContext : DbContext
>>>>>>> Stashed changes
    {
        public BkpsContext()
        {
        }

        public BkpsContext(DbContextOptions<BkpsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<AppConfig> AppConfigs { get; set; }

        public virtual DbSet<FeedBack> FeedBacks { get; set; }

        public virtual DbSet<ListParty> ListParties { get; set; }

        public virtual DbSet<ListProduct> ListProducts { get; set; }

        public virtual DbSet<ListRoom> ListRooms { get; set; }

        public virtual DbSet<Party> Parties { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=(local);Database=BKPS;uid=sa;pwd=12345;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Account__1788CC4CC9BCAAD2");

                entity.ToTable("Account");

                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.AvatarUrl).HasMaxLength(500);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FullName).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasMaxLength(255);
                entity.Property(e => e.Status).HasMaxLength(255);
                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<AppConfig>(entity =>
            {
                entity.HasKey(e => e.Key).HasName("PK__AppConfi__C41E0288FAA7AA0E");

                entity.ToTable("AppConfig");

                entity.Property(e => e.Key).HasMaxLength(50);
                entity.Property(e => e.Value).HasMaxLength(500);
            });

            modelBuilder.Entity<FeedBack>(entity =>
            {
                entity.HasKey(e => e.FeedBackId).HasName("PK__FeedBack__E2CB3B87680F6CD7");

                entity.ToTable("FeedBack");

                entity.Property(e => e.Feedback1)
                    .HasMaxLength(2000)
                    .HasColumnName("Feedback");

                entity.HasOne(d => d.Party).WithMany(p => p.FeedBacks)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__FeedBack__PartyI__4BAC3F29");
            });

            modelBuilder.Entity<ListParty>(entity =>
            {
                entity.HasKey(e => e.ListPartyId).HasName("PK__ListPart__CD4E048418CB9191");

                entity.ToTable("ListParty");

                entity.Property(e => e.ListPartyStatus).HasMaxLength(500);

                entity.HasOne(d => d.Party).WithMany(p => p.ListParties)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__ListParty__Party__300424B4");
            });

            modelBuilder.Entity<ListProduct>(entity =>
            {
                entity.HasKey(e => e.ListProductId).HasName("PK__ListProd__BB3E314FB6B6F515");

                entity.ToTable("ListProduct");

                entity.Property(e => e.ListProductStatus).HasMaxLength(500);

                entity.HasOne(d => d.Party).WithMany(p => p.ListProducts)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__ListProdu__Party__30F848ED");

                entity.HasOne(d => d.Product).WithMany(p => p.ListProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ListProdu__Produ__32E0915F");

                entity.HasOne(d => d.Room).WithMany(p => p.ListProducts)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__ListProdu__RoomI__31EC6D26");
            });

            modelBuilder.Entity<ListRoom>(entity =>
            {
                entity.HasKey(e => e.ListRoomId).HasName("PK__ListRoom__226F7FF0EC86AC4E");

                entity.ToTable("ListRoom");

                entity.Property(e => e.ListRoomStatus).HasMaxLength(500);

                entity.HasOne(d => d.Party).WithMany(p => p.ListRooms)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("FK__ListRoom__PartyI__33D4B598");

                entity.HasOne(d => d.Room).WithMany(p => p.ListRooms)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__ListRoom__RoomId__34C8D9D1");
            });

            modelBuilder.Entity<Party>(entity =>
            {
                entity.HasKey(e => e.PartyId).HasName("PK__Party__1640CD332E236050");

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
                entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD99786F47");

                entity.ToTable("Product");

                entity.Property(e => e.ProductName).HasMaxLength(255);
                entity.Property(e => e.ProductStatus).HasMaxLength(500);
                entity.Property(e => e.ProductStyle).HasMaxLength(100);
                entity.Property(e => e.ProductType).HasMaxLength(50);
                entity.Property(e => e.ProductUrl).HasMaxLength(1000);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId).HasName("PK__Room__328639393E1F6E59");

                entity.ToTable("Room");

                entity.Property(e => e.RoomName).HasMaxLength(255);
                entity.Property(e => e.RoomStatus).HasMaxLength(500);
                entity.Property(e => e.RoomType).HasMaxLength(50);
                entity.Property(e => e.RoomUrl).HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}