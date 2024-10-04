using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Threading.Tasks.Dataflow;

namespace ApiManeroProduct.Entities;

public class ProductEntity
{
    [Key]
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Title { get; set; } = null!;
    public string? BatchNumber { get; set; } = null!;
    public string? Category { get; set; }
    public string PartitionKey { get; set; } = "Products";
    public string? Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public string? DiscountPrice { get; set; }
    public string? Color { get; set; } = null!;
    public string? Size { get; set; }
    public string? ImageUrl { get; set; } 
    public DateTime? Created { get; set; }
    public DateTime? LastUpdated { get; set; }
}
