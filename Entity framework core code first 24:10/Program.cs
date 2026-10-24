using EntityFrameworkCoreCodeFirst.Data;
using EntityFrameworkCoreCodeFirst.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;

using var db = new AppDbContext();

// Tạo database và áp dụng migration (nếu chưa có)
db.Database.Migrate();

// Thêm mới 1 danh mục kèm sản phẩm
if (!db.Categories.Any(c => c.Name == "Máy tính bảng"))
{
    var tablet = new Category
    {
        Name = "Máy tính bảng",
        Description = "Các loại tablet",
        Products = new List<Product>
        {
            new Product { Name = "iPad Pro 11", Price = 25000000, Quantity = 8 }
        }
    };
    db.Categories.Add(tablet);
    db.SaveChanges();
}

// Truy vấn: danh mục và các sản phẩm của nó
var categories = db.Categories.Include(c => c.Products).ToList();

foreach (var c in categories)
{
    Console.WriteLine($"[{c.CategoryId}] {c.Name}");
    foreach (var p in c.Products)
    {
        Console.WriteLine($"    - {p.Name} | {p.Price:N0} đ | SL: {p.Quantity}");
    }
}
