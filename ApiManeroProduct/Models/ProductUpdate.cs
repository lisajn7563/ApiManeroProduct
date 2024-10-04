namespace ApiManeroProduct.Models;

public class ProductUpdate
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Title { get; set; } = null!;
    public string? BatchNumber { get; set; } = null!;
    public string? Description { get; set; }
    public decimal OriginalPrice { get; set; }
    public string? Category { get; set; } 
    public string? DiscountPrice { get; set; }
    public string? ImageUrl { get; set; } 
    public string? Color { get; set; } = null!;
    public string? Size { get; set; } = null!;
}
