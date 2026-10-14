using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockPulse.Server.Auxiliars;
using StockPulse.Server.Contexts;
using StockPulse.Server.Entities;
using StockPulse.Server.Repositories;
using System.Text.Json;

namespace StockPulse.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductDto> _validator;

        public ProductService(IProductRepository productRepository, IValidator<CreateProductDto> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Result<Product?>> GetById(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Result<Product?>.Failure($"Product with ID {id} not found.");
            }
            return Result<Product?>.Success(product);
        }

        public async Task<Result<Product>> Add(CreateProductDto? createProductDto)
        {

            if (createProductDto == null)
            {
                return Result<Product>.Failure("CreateProductDto cannot be null.");
            }
            
            var validation = _validator.Validate(createProductDto);
            
            if (!validation.IsValid)
            {
                return Result<Product>.Failure(JsonSerializer.Serialize(validation.Errors));
            }

            var newProduct = await _productRepository.AddProductAsync(createProductDto);
            return Result<Product>.Success(newProduct);
        }

    }
}
