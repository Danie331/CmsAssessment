using Cms.Data.DAL.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Cms.Data.DAL.Context
{
    public class CmsStockManagmentContext : DbContext
    {
        public CmsStockManagmentContext()
        {
        }

        public CmsStockManagmentContext(DbContextOptions<CmsStockManagmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<StockAccessory> StockAccessory { get; set; }
        public virtual DbSet<StockItem> StockItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=CmsStockManagment;Integrated security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.StockItem)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.StockItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Image__StockItem__286302EC");
            });

            modelBuilder.Entity<StockAccessory>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.StockItem)
                    .WithMany(p => p.StockAccessory)
                    .HasForeignKey(d => d.StockItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__StockAcce__Stock__29572725");
            });

            modelBuilder.Entity<StockItem>(entity =>
            {
                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CostPrice).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Dtcreated)
                    .HasColumnName("DTCreated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dtupdated)
                    .HasColumnName("DTUpdated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Kms).HasColumnName("KMS");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RetailPrice).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasColumnName("VIN")
                    .HasMaxLength(17)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
    }
}
