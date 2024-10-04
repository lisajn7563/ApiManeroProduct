namespace Infrastructure.Models;

public class Product
{
    public string ProductId { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public string? Image { get; set; }
    public string Size { get; set; } = null!;
    public Rating? Rating { get; set; }
    public Tags Tags { get; set; } = null!;
    public string? Reviews { get; set; }
    public bool Sale { get; set; }
    public bool New { get; set; }
    public bool Top { get; set; }
}
