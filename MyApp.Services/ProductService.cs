using AutoMapper;
using MyApp.Core.DTOs;
using MyApp.Core.Entities;
using MyApp.Core.Exceptions;
using MyApp.Core.Interfaces;
using MyApp.Data.Repositories;

namespace MyApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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
                throw new NotFoundException(nameof(Product), id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedDate = DateTime.UtcNow;
            product.IsActive = true;

            var createdProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                throw new NotFoundException(nameof(Product), id);

            _mapper.Map(updateProductDto, existingProduct);
            existingProduct.UpdatedDate = DateTime.UtcNow;

            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException(nameof(Product), id);

            await _productRepository.DeleteAsync(product);
        }
    }
}

