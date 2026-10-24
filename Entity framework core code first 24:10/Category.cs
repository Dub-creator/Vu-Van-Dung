using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreCodeFirst.Models;

/// <summary>
/// Lớp thực thể "Danh mục" -> bảng Categories
/// </summary>
[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    // Navigation property: 1 danh mục có nhiều sản phẩm
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
