using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;

namespace MyApp.Data
{
    public class MyAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product entity configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Category).HasMaxLength(50);
                
                // Seed data
                entity.HasData(new Product
                {
                    Id = 1,
                    Name = "Sample Product 1",
                    Description = "This is a sample product description",
                    Price = 99.99m,
                    Stock = 100,
                    Category = "Electronics",
                    CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                });
            });

            // Log entity configuration
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Logs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Application).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Logged).IsRequired();
                entity.Property(e => e.Level).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Message).IsRequired().HasColumnType("nvarchar(max)");
                entity.Property(e => e.Logger).HasMaxLength(250);
                entity.Property(e => e.Callsite).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Exception).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Properties).HasColumnType("nvarchar(max)");

                // Index'ler - Performans için
                entity.HasIndex(e => e.Logged).HasDatabaseName("IX_Logs_Logged");
                entity.HasIndex(e => e.Level).HasDatabaseName("IX_Logs_Level");
                entity.HasIndex(e => e.Logger).HasDatabaseName("IX_Logs_Logger");
                entity.HasIndex(e => new { e.Logged, e.Level }).HasDatabaseName("IX_Logs_Logged_Level");
            });
        }
    }
}

