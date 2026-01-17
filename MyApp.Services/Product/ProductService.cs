using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp.Services.Product.DTOs;
using MyApp.Services.Product.Interfaces;
using MyApp.Services.Shared.Exceptions;
using MyApp.Data.Product.Interfaces;

namespace MyApp.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository, 
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException(nameof(Data.Product.Entities.Product), id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Data.Product.Entities.Product>(createProductDto);
            product.CreatedDate = DateTime.UtcNow;
            product.IsActive = true;

            var createdProduct = await _productRepository.AddAsync(product);

            _logger.LogInformation(
                "Product created successfully. Id: {ProductId}, Name: {ProductName}",
                createdProduct.Id, createdProduct.Name);

            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                _logger.LogWarning(
                    "Product not found for update. Id: {ProductId}",
                    id);
                throw new NotFoundException(nameof(Data.Product.Entities.Product), id);
            }

            _mapper.Map(updateProductDto, existingProduct);
            existingProduct.UpdatedDate = DateTime.UtcNow;

            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);

            _logger.LogInformation(
                "Product updated successfully. Id: {ProductId}, Name: {ProductName}",
                updatedProduct.Id, updatedProduct.Name);

            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning(
                    "Product not found for deletion. Id: {ProductId}",
                    id);
                throw new NotFoundException(nameof(Data.Product.Entities.Product), id);
            }

            await _productRepository.DeleteAsync(product);

            _logger.LogInformation(
                "Product deleted successfully. Id: {ProductId}, Name: {ProductName}",
                id, product.Name);
        }
    }
}
