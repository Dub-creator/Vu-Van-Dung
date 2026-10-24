using EntityFrameworkCoreCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreCodeFirst.Data;

public class AppDbContext : DbContext
{
    // Mỗi DbSet tương ứng một bảng trong database
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Tên database: EntityFrameworkCoreCodeFirst
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;
              Database=EntityFrameworkCoreCodeFirst;
              Trusted_Connection=True;
              TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình quan hệ 1-n bằng Fluent API
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tên danh mục là duy nhất
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        // Dữ liệu mẫu (seed data)
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Điện thoại", Description = "Các loại điện thoại thông minh" },
            new Category { CategoryId = 2, Name = "Laptop", Description = "Máy tính xách tay" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "iPhone 15", Price = 22000000, Quantity = 10, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1) },
            new Product { ProductId = 2, Name = "Samsung Galaxy S24", Price = 19000000, Quantity = 15, CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1) },
            new Product { ProductId = 3, Name = "MacBook Air M2", Price = 28000000, Quantity = 5, CategoryId = 2, CreatedAt = new DateTime(2024, 1, 1) }
        );
    }
}
