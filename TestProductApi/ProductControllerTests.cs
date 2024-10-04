using ApiManeroProduct.Contexts;
using ApiManeroProduct.Controllers;
using ApiManeroProduct.Entities;
using ApiManeroProduct.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProductApi;

public class ProductControllerTests
{
    private readonly DbContextOptions<DataContext> _options;
    private readonly DataContext _context;

    public ProductControllerTests()
    {
        // Använd InMemory-databas för testning
        _options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new DataContext(_options);

        // Seed data if needed
        SeedData();
    }

    private void SeedData()
    {
        // Seed your test data here if needed
        _context.Products.AddRange(new List<ProductEntity>
            {
                new ProductEntity { Id = "1", Title = "Test Product 1", BatchNumber = "B001", OriginalPrice = 10.0M, Category = "MEN" },
                new ProductEntity { Id = "2", Title = "Test Product 2", BatchNumber = "B002", OriginalPrice = 20.0M, Category = "WOMEN" }
            });
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllProducts()
    {
        // Arrange
        var controller = new ProductController(_context);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsType<List<ProductEntity>>(okResult.Value);
        Assert.Equal(2, products.Count);
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_IfExists()
    {
        // Arrange
        var controller = new ProductController(_context);

        // Act
        var result = await controller.GetById("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var product = Assert.IsType<ProductEntity>(okResult.Value);
        Assert.Equal("Test Product 1", product.Title);
    }

    [Fact]
    public async Task Create_ShouldAddProduct_WhenModelIsValid()
    {
        // Arrange
        var controller = new ProductController(_context);
        var newProduct = new ProductRegistration
        {
            Title = "New Product",
            BatchNumber = "B003",
            OriginalPrice = 30.0M,
            Category = "MEN"
        };

        // Act
        var result = await controller.Create(newProduct);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var product = Assert.IsType<ProductEntity>(okResult.Value);
        Assert.Equal("New Product", product.Title);
        Assert.Equal(3, _context.Products.Count());
    }

    [Fact]
    public async Task Delete_ShouldRemoveProduct_IfExists()
    {
        // Arrange
        var controller = new ProductController(_context);

        // Act
        var result = await controller.Delete("1");

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Equal(1, _context.Products.Count()); // Kontrollera att produkten har tagits bort
        var product = await _context.Products.FindAsync("1");
        Assert.Null(product); // Kontrollera att produkten inte längre finns i databasen
    }

    [Fact]
    public async Task Update_ShouldModifyProduct_IfExists()
    {
        // Arrange
        var controller = new ProductController(_context);
        var updatedProduct = new ProductUpdate
        {
            Title = "Updated Product",
            BatchNumber = "B001",
            Description = "Updated Description",
            OriginalPrice = 15.0M,
            DiscountPrice = "10.0",
            Color = "Red",
            Size = "M",
            ImageUrl = "http://example.com/updated-image.jpg",
            Category = "MEN"
        };
        // Act
        var result = await controller.Update("1", updatedProduct);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var product = await _context.Products.FindAsync("1");
        Assert.NotNull(product);
        Assert.Equal("Updated Product", product.Title);
        Assert.Equal("Updated Description", product.Description);
        Assert.Equal(15.0M, product.OriginalPrice);
        Assert.Equal("10.0", product.DiscountPrice);
        Assert.Equal("Red", product.Color);
        Assert.Equal("M", product.Size);
        Assert.Equal("http://example.com/updated-image.jpg", product.ImageUrl);
        Assert.Equal("MEN", product.Category);
    }

    [Fact]
    public void GetByBatchNumber_ShouldReturnProducts_IfBatchNumberExists()
    {
        // Arrange
        var controller = new ProductController(_context);

        // Act
        var result = controller.GetByBatchNumber("B001");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var products = Assert.IsType<List<ProductRegistration>>(okResult.Value);
        Assert.Single(products);
        Assert.Equal("Test Product 1", products[0].Title);
    }

    [Fact]
    public void GetByBatchNumber_ShouldReturnNotFound_IfBatchNumberDoesNotExist()
    {
        // Arrange
        var controller = new ProductController(_context);

        // Act
        var result = controller.GetByBatchNumber("B999");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetByBatchNumber_ShouldReturnBadRequest_IfBatchNumberIsEmpty()
    {
        // Arrange
        var controller = new ProductController(_context);

        // Act
        var result = controller.GetByBatchNumber("");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BatchNumber måste vara specificerat.", badRequestResult.Value);
    }
}
