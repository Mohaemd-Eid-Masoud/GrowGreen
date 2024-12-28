using GrowGreen.Domain.Entities;
using GrowGreen.Domain.Interfaces;
using GrowGreen.Application.DTOs;

namespace GrowGreen.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Get all products and return them as DTOs
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            // Map Product entities to ProductDto
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
        }

        // Get a product by its ID and return it as DTO
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            // Map Product entity to ProductDto
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        // Create a new product
        public async Task CreateProductAsync(ProductDto productDto)
        {
            // Perform any validation or business logic
            if (string.IsNullOrEmpty(productDto.Name))
                throw new Exception("Product name is required");

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                ImagePath = productDto.ImagePath,
                Category = productDto.Category,
                Description = productDto.Description,
            };

            await _productRepository.AddAsync(product);
        }

        // Update an existing product
        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            // Perform business logic, e.g., validating price change
            if (productDto.Price < 0)
                throw new Exception("Price must be a positive value");

            product.Name = productDto.Name;
            product.Price = productDto.Price;

            _productRepository.UpdateAsync(product);
        }

        // Delete a product
        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Product not found");

            await _productRepository.DeleteAsync(id);
        }
    }
}
