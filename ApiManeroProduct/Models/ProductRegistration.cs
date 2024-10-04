using ApiManeroProduct.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiManeroProduct.Models;

public class ProductRegistration
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Title { get; set; } = null!;
    public string? BatchNumber { get; set; } = null!;
    public string? Description { get; set; } = "";
    public decimal OriginalPrice { get; set; }
    public string? Category { get; set; } 
    public string? DiscountPrice { get; set; }
    public string? ImageUrl { get; set; }
    public string? Color { get; set; } = null!;
    public string? Size { get; set; } = null!;
    public DateTime? Created { get; set; }
}
//public class ProductEntity
//{

//    [Key]
//    public string Id { get; set; } = Guid.NewGuid().ToString();
//    public string Title { get; set; } = null!;
//    public string BatchNumber { get; set; } = null!;

//    public List<string> Categories { get; set; } = null!;

//    public string PartitionKey { get; set; } = "Products";
//    public string? Description { get; set; }
//    public decimal OriginalPrice { get; set; }
//    public string Color { get; set; } = null!;
//    public string? ImageUrl { get; set; }
//    public string Size { get; set; } = null!;

//    public DateTime Created { get; set; }
//    public DateTime LastUpdated { get; set; }
//}

//public class Color
//{
//    [Key]
//    public string id { get; set; } = Guid.NewGuid().ToString();
//    public string colorName { get; set; } = null!;
//}

//public class Size
//{
//    [Key]
//    public string id { get; set; } = Guid.NewGuid().ToString();
//    public string sizeName { get; set; } = null!;
//}

//public class Category
//{
//    [Key]
//    public string id { get; set; } = Guid.NewGuid().ToString();
//    public string categorTitle { get; set; } = null!;
//}