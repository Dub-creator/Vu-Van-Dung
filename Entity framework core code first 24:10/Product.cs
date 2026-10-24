using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreCodeFirst.Models;

/// <summary>
/// Lớp thực thể "Sản phẩm" -> bảng Products
/// </summary>
[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Khóa ngoại tới Category
    public int CategoryId { get; set; }

    // Navigation property: mỗi sản phẩm thuộc 1 danh mục
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
}
