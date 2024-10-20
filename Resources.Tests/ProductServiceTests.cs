using FluentAssertions;
using Resources.Enums;
using Resources.Models;
using Resources.Services;

namespace Resources.Tests;

public class ProductServiceTests : IProductServiceTests
{
    [Fact]
    public void AddToList__Should__ReturnSuccess__WhenProductIsAddedToList()
    {
        // Arrange
        Product product = new Product();
        ProductService productService = new ProductService();

        // Act
        var result = productService.AddToList(product);
        var productList = productService.GetAllProducts();

        // Assert        
        ResultStatus.Success.Should().HaveSameValueAs(result);
        productList.Should().NotBeEmpty();
        productList.Should().HaveCount(1).And.ContainSingle(p => p.Id == product.Id);
    }
    [Fact]
    public void DeleteAllProducts__Should__ReturnSuccess__WhenAllProductsAreDeletedFromList()
    {
        // Arrange 
        ProductService productService = new ProductService();
        productService.AddToList(new Product { Id = "1", Name = "Test Name", Price = 1, }); // Lägger till en produkt för att säkerställa att listan inte är tom.


        // Act
        var result = productService.DeleteAllProducts();
        var productList = productService.GetAllProducts();

        // Assert
        result.Should().Be(ResultStatus.Success);
        productList.Should().BeEmpty();
    }
}
