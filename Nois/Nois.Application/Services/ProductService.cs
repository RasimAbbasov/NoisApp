using AutoMapper;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBlobStorageService _blobStorageService;
        private const string containerName = "product-images"; // Define your container name here

        public ProductService(IMapper mapper,IGenericRepository<Product> genericRepository,IProductRepository productRepository,IBlobStorageService blobStorageService,IGenericRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _productRepository = productRepository;
            _blobStorageService = blobStorageService;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CreateProductDto createProductDto)
        {
            if (createProductDto == null)
                throw new ArgumentNullException(nameof(createProductDto));

            var nameExists = await _genericRepository.ExistsAsync(x => x.Name == createProductDto.Name);
            if (nameExists)
                throw new ConflictException("Product with this name already exists.");

            var categoryExists = await _categoryRepository.ExistsAsync(x => x.Id == createProductDto.CategoryId);
            if (!categoryExists)
                throw new KeyNotFoundException($"Category with ID {createProductDto.CategoryId} does not exist.");

            var product = _mapper.Map<Product>(createProductDto);
            product.CreatedAt = DateTime.UtcNow;

            if (createProductDto.ImageFile != null && createProductDto.ImageFile.Length > 0)
            {
                product.BlobName = await _blobStorageService.UploadFileAsync(containerName, createProductDto.ImageFile);
            }
            await _genericRepository.CreateAsync(product);
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found.");

            await _genericRepository.DeleteAsync(product);
        }

        public async Task<List<ProductSummaryDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllWithIncludes();
            return _mapper.Map<List<ProductSummaryDto>>(products);
        }

        public async Task<ProductSummaryDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdWithIncludes(id);
            if (product == null) throw new KeyNotFoundException($"Item with id {id} not found");

            return _mapper.Map<ProductSummaryDto>(product);
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
                throw new ArgumentNullException(nameof(updateProductDto));

            var entity = await _genericRepository.GetByIdAsync(updateProductDto.Id);
            if (entity == null)
                throw new KeyNotFoundException($"Product with ID {updateProductDto.Id} not found.");

            // Exclude current entity from name conflict check
            var nameExists = await _genericRepository.ExistsAsync(x => x.Name == updateProductDto.Name && x.Id != updateProductDto.Id);
            if (nameExists)
                throw new ConflictException("Product with this name already exists.");

            var categoryExists = await _categoryRepository.ExistsAsync(x => x.Id == updateProductDto.CategoryId);
            if (!categoryExists)
                throw new KeyNotFoundException($"Category with ID {updateProductDto.CategoryId} does not exist.");

            // Map the basic properties first (exclude BlobName/ImageFile in mapping)
            _mapper.Map(updateProductDto, entity);

            // Handle image update
            if (updateProductDto.ImageFile != null && updateProductDto.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(entity.BlobName))
                    await _blobStorageService.DeleteFileAsync(containerName, entity.BlobName);

                entity.BlobName = await _blobStorageService.UploadFileAsync(containerName, updateProductDto.ImageFile);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            await _genericRepository.UpdateAsync(entity);
        }
    }
}
