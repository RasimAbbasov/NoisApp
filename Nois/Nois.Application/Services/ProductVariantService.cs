using AutoMapper;
using Nois.Application.DTOs.CategoryDTOs;
using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Common;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class ProductVariantService : IProductVariantService
    {
        public IGenericRepository<ProductVariant> _genericRepository;
        public IProductVariantRepository _productVariantRepository;
        public IGenericRepository<Color> _colorRepository;
        public IGenericRepository<Product> _productRepository;
        public IGenericRepository<Size> _sizeRepository;
        public IMapper _mapper;
        public ProductVariantService(IGenericRepository<ProductVariant> genericRepository,IProductVariantRepository productVariantRepository,IGenericRepository<Color> colorRepository,IGenericRepository<Product> productRepository,IGenericRepository<Size> sizeRepository , IMapper mapper)
        {
            _genericRepository = genericRepository;
            _productVariantRepository = productVariantRepository;
            _colorRepository = colorRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateProductVariantDto createProductVariantDto)
        {
            if (createProductVariantDto == null)
                throw new ArgumentNullException(nameof(createProductVariantDto));

            var colorExists = await _colorRepository.ExistsAsync(x => x.Id == createProductVariantDto.ColorId);
            if (!colorExists)
                throw new KeyNotFoundException($"Color with ID {createProductVariantDto.ColorId} does not exist.");

            var productExists = await _productRepository.ExistsAsync(x => x.Id == createProductVariantDto.ProductId);
            if (!productExists)
                throw new KeyNotFoundException($"Product with ID {createProductVariantDto.ProductId} does not exist.");

            var sizeExists = await _sizeRepository.ExistsAsync(x => x.Id == createProductVariantDto.SizeId);
            if (!sizeExists)
                throw new KeyNotFoundException($"Size with ID {createProductVariantDto.SizeId} does not exist.");

            var productVariant = _mapper.Map<ProductVariant>(createProductVariantDto);
            productVariant.CreatedAt = DateTime.UtcNow;

            await _genericRepository.CreateAsync(productVariant);
        }

        public async Task DeleteAsync(int id)
        {
            var productVariant = await _genericRepository.GetByIdAsync(id);
            if (productVariant == null) throw new KeyNotFoundException("ProductVariant not found.");

            await _genericRepository.DeleteAsync(productVariant);
        }

        public async Task<List<ProductVariantSummaryDto>> GetAllAsync()
        {
            var productVariants = await _productVariantRepository.GetAllWithIncludes();   
            return _mapper.Map<List<ProductVariantSummaryDto>>(productVariants);
        }

        public async Task<ProductVariantSummaryDto> GetByIdAsync(int id)
        {
            var productVariant = await _productVariantRepository.GetByIdWithIncludes(id);
            if (productVariant == null) throw new KeyNotFoundException($"Item with id {id} not found");
            return _mapper.Map<ProductVariantSummaryDto>(productVariant);
        }
		public async Task<PaginationResult<ProductVariantSummaryDto>> GetPagedAsync(PaginationRequest request)
		{
			// Get paginated entities from repository
			var pagedProductVariants = await _productVariantRepository.GetPagedAsync(request);

			// Map entities → DTOs
			var dtoList = _mapper.Map<List<ProductVariantSummaryDto>>(pagedProductVariants.Items);

			// Return paginated DTO result
			return new PaginationResult<ProductVariantSummaryDto>(
				dtoList,
				pagedProductVariants.Page,
				pagedProductVariants.PageSize,
				pagedProductVariants.TotalRecords
			);
		}

		public async Task UpdateAsync(UpdateProductVariantDto updateProductVariantDto)
        {
            if (updateProductVariantDto == null)
                throw new ArgumentNullException(nameof(updateProductVariantDto));

            var entity = await _genericRepository.GetByIdAsync(updateProductVariantDto.Id);
            if (entity == null)
                throw new KeyNotFoundException($"Product with ID {updateProductVariantDto.Id} not found.");

            var colorExists = await _colorRepository.ExistsAsync(x => x.Id == updateProductVariantDto.ColorId);
            if (!colorExists)
                throw new KeyNotFoundException($"Color with ID {updateProductVariantDto.ColorId} does not exist.");

            var productExists = await _productRepository.ExistsAsync(x => x.Id == updateProductVariantDto.ProductId);
            if (!productExists)
                throw new KeyNotFoundException($"Product with ID {updateProductVariantDto.ProductId} does not exist.");

            var sizeExists = await _sizeRepository.ExistsAsync(x => x.Id == updateProductVariantDto.SizeId);
            if (!sizeExists)
                throw new KeyNotFoundException($"Size with ID {updateProductVariantDto.SizeId} does not exist.");

            // Map the basic properties first (exclude BlobName/ImageFile in mapping)
            _mapper.Map(updateProductVariantDto, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            await _genericRepository.UpdateAsync(entity);
        }
    }
}
