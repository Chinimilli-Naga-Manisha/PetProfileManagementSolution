using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PetProfileManagementBackend.Controllers;
using PetProfileManagementBackend.Models;
using PetProfileManagementBackend.Services;

namespace PetProfileManagementBackend.Tests
{
    public class ProductControllerSimpleTests
    {
        private readonly ProductController _controller;
        private readonly Mock<ProductService> _serviceMock;

        public ProductControllerSimpleTests()
        {
            _serviceMock = new Mock<ProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        // 3. Create Product Test
        [Fact]
        public void CreateProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newProduct = new Product { Id = 1, Name = "NewProduct", Price = 50 };

            // Act
            var result = _controller.CreateProduct(newProduct);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
        // 4. Update Product - Id Mismatch (BadRequest)
        [Fact]
        public void UpdateProduct_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var product = new Product { Id = 2, Name = "Product2", Price = 200 };

            // Act
            var result = _controller.UpdateProduct(1, product);  // ID mismatch!

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        // 9. CreateProduct - Adds product and returns correct route values
        [Fact]
        public void CreateProduct_ReturnsCreatedAtAction_WithCorrectProduct()
        {
            // Arrange
            var newProduct = new Product { Id = 5, Name = "NewProduct", Price = 150 };

            // Act
            var result = _controller.CreateProduct(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnProduct = Assert.IsType<Product>(createdAtActionResult.Value);

            Assert.Equal(newProduct.Id, returnProduct.Id);
            Assert.Equal("NewProduct", returnProduct.Name);
        }
        [Fact]
        public void CreateProduct_ValidProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockService = new Mock<ProductService>();
            var controller = new ProductController(mockService.Object);

            var newProduct = new Product { Id = 1, Name = "Test Product" };

            // Act
            var result = controller.CreateProduct(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnProduct = Assert.IsType<Product>(createdAtActionResult.Value);

            Assert.Equal(newProduct.Id, returnProduct.Id);
            Assert.Equal("Test Product", returnProduct.Name);
        }

    }
}
