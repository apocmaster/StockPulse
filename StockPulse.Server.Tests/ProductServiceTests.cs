using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using StockPulse.Server.Entities;
using StockPulse.Server.Repositories;
using StockPulse.Server.Services;
using System.Text.Json;

namespace StockPulse.Server.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddProductAsync_Returns_Success_Result_When_Valid_Dto_Is_Provided()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var validator = Substitute.For<IValidator<CreateProductDto>>();

            var createProductDto = new CreateProductDto
            {
                Name = "Test Product",
                SKU = "TEST"
            };

            validator.Validate(Arg.Any<CreateProductDto>())
                .Returns(new ValidationResult());

            repository.AddProductAsync(Arg.Any<CreateProductDto>())
                .Returns(Task.FromResult(new Product { Id = Guid.NewGuid(), Name = createProductDto.Name, SKU = createProductDto.SKU }));

            var productService = new ProductService(repository, validator);

            // Act
            var result = await productService.Add(createProductDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(createProductDto.Name, result.Value.Name);
            Assert.Equal(createProductDto.SKU, result.Value.SKU);
        }

        [Fact]
        public async Task AddProductAsync_Returns_Failure_Result_When_Invalid_Dto_Is_Provided()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var validator = Substitute.For<IValidator<CreateProductDto>>();

            var validationResult = new ValidationResult(new[]
            {
                new ValidationFailure("Name", "Name is required"),
                new ValidationFailure("SKU", "SKU is required")
            });

            validator.Validate(Arg.Any<CreateProductDto>())
            .Returns(validationResult);

            var createProductDto = new CreateProductDto
            {
                Name = null,
                SKU = null
            };
           
            var productService = new ProductService(repository, validator);

            // Act
            var result = await productService.Add(createProductDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
            Assert.Equal(JsonSerializer.Serialize(validationResult.Errors), result.ErrorMessage);
        }

        [Fact]
        public async Task AddProductAsync_Returns_Failure_Result_When_Dto_Is_Null()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var validator = Substitute.For<IValidator<CreateProductDto>>();

            var productService = new ProductService(repository, validator);

            // Act
            var result = await productService.Add(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
            Assert.Equal("CreateProductDto cannot be null.", result.ErrorMessage);
        }

        [Fact]
        public async Task GetProductByIdAsync_Returns_Product_When_Exists()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var validator = Substitute.For<IValidator<CreateProductDto>>();

            var product = new Product { Name = "Test Product", SKU = "TEST" };
            repository.GetProductByIdAsync(product.Id).Returns(product);

            var productService = new ProductService(repository, validator);

            // Act
            var result = await productService.GetById(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Value!.Id);
            Assert.Equal(product.Name, result.Value.Name);
            Assert.Equal(product.SKU, result.Value.SKU);
        }

        [Fact]
        public async Task GetProductByIdAsync_Returns_Null_When_Not_Exists()
        {
            // Arrange
            var repository = Substitute.For<IProductRepository>();
            var validator = Substitute.For<IValidator<CreateProductDto>>();

            var product = new Product { Name = "Test Product", SKU = "TEST" };
            repository.GetProductByIdAsync(product.Id).Returns((Product?)null);

            var productService = new ProductService(repository, validator);

            // Act
            var result = await productService.GetById(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.Equal($"Product with ID {product.Id} not found.", result.ErrorMessage);
        }
    }
}
