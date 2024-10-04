using ApiManeroProduct.Contexts;
using ApiManeroProduct.Entities;
using ApiManeroProduct.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ApiManeroProduct.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Create(ProductRegistration model)
    {
        if (ModelState.IsValid)
        {
            var entity = new ProductEntity
            {
                Title = model.Title,
                BatchNumber = model.BatchNumber,
                Description = model.Description,
                OriginalPrice = model.OriginalPrice,
                Color = model.Color,
                Size = model.Size,
                ImageUrl = model.ImageUrl,
                Created = model.Created,
                DiscountPrice = model.DiscountPrice,
                Category = model.Category,
            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        return BadRequest();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
       var productList = await _context.Products.ToListAsync();
        
        return Ok(productList);   

    }

    // Hämta en produkt baserad på ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(); // Returnera 404 om produkten inte hittades
        }

        return Ok(product); // Returnera produkten om den hittades
    }
    // Uppdatera en produkt baserad på ID
    [HttpPut("{id}")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Update(string id, ProductUpdate model)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(); // Returnera 404 om produkten inte hittades
        }

        product.Title = model.Title;
        product.BatchNumber = model.BatchNumber;
        product.Description = model.Description;
        product.OriginalPrice = model.OriginalPrice;
        product.Color = model.Color;
        product.Size = model.Size;
        product.ImageUrl = model.ImageUrl;
        product.Category = model.Category;
        product.DiscountPrice = model.DiscountPrice;


        // Uppdatera databasen
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return NoContent(); // Returnera 204 No Content efter att produkten har uppdaterats
    }

    // Radera en produkt baserad på ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(); // Returnera 404 om produkten inte hittades
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent(); // Returnera 204 No Content efter att produkten har raderats
    }

    [HttpGet("GetByBatchNumber")]
    public IActionResult GetByBatchNumber(string batchNumber)
    {
        if (string.IsNullOrEmpty(batchNumber))
        {
            return BadRequest("BatchNumber måste vara specificerat.");
        }

        var products = _context.Products.Where(p => p.BatchNumber == batchNumber).ToList();

        if (products == null || !products.Any())
        {
            return NotFound(); // Returnera 404 om inga produkter hittades för det angivna batchnumret
        }

        var combinedProduct = MapProductToRegProd(products);

        return Ok (combinedProduct);
    }

    public static List<ProductRegistration> MapProductToRegProd(List<ProductEntity> products)
    {
        List<ProductRegistration> result = new List<ProductRegistration>();

        foreach (var product in products)
        {
            result.Add(new ProductRegistration
            {
                Title = product.Title,
                BatchNumber = product.BatchNumber,
                Description = product.Description,
                OriginalPrice = product.OriginalPrice,
                DiscountPrice = product.DiscountPrice,
                Created = product.Created,
                Category = product.Category,
                Color = product.Color,
                Size = product.Size,
                ImageUrl = product.ImageUrl
            });
        }
        return result;
    }
}
