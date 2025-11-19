using AutoMapper;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Persistance.Repositories.Interfaces;

namespace Nois.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IMapper mapper,IGenericRepository<Product> productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task CreateAsync(CreateProductDto createProductDto)
        {
            //FILE UPLOAD HISSESI YAZILMALIDIR!

            if (createProductDto == null)
                throw new ArgumentNullException(nameof(createProductDto));

            var exists = await _productRepository.ExistsAsync(x => x.Name == createProductDto.Name);
            if (exists)
                throw new ConflictException("Product with this name already exists.");

            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.Now;
            await _productRepository.CreateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id), "Id must be greater than zero.");

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found.");

            await _productRepository.DeleteAsync(product);
        }

        public async Task<List<ProductSummaryDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductSummaryDto>>(products);
        }

        public async Task<ProductSummaryDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException($"Item with id {id} not found");

            return _mapper.Map<ProductSummaryDto>(product);
        }

        public Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
