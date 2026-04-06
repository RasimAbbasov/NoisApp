using AutoMapper;
using Nois.Application.DTOs.ProductDtos;
using Nois.Application.DTOs.ProductStockDtos;
using Nois.Application.DTOs.ProductVariantDtos;
using Nois.Application.Interfaces;
using Nois.Domain.Common;
using Nois.Domain.Entities;
using Nois.Domain.Interfaces;

namespace Nois.Application.Services
{
    public class ProductStockService : IProductStockService
    {
        public IGenericRepository<ProductStock> _genericRepository;
        public IProductStockRepository _productStockRepository;
        public IGenericRepository<ProductVariant> _productVariantRepository;
        public IMapper _mapper;
        public ProductStockService(IGenericRepository<ProductStock> genericRepository,IProductStockRepository productStockRepository,IGenericRepository<ProductVariant> productVariantRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _productStockRepository = productStockRepository;
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateProductStockDto createProductStockDto)
        {
            if (createProductStockDto == null)
                throw new ArgumentNullException(nameof(createProductStockDto));

            var productExists = await _productVariantRepository.ExistsAsync(x => x.Id == createProductStockDto.ProductVariantId);
            if (!productExists)
                throw new KeyNotFoundException($"ProductVariant with ID {createProductStockDto.ProductVariantId} does not exist.");

            var productStock = _mapper.Map<ProductStock>(createProductStockDto);
            productStock.CreatedAt = DateTime.UtcNow;

            await _genericRepository.CreateAsync(productStock);
        }

        public async Task DeleteAsync(int id)
        {
          var productStock = await _genericRepository.GetByIdAsync(id);

            if (productStock == null) throw new KeyNotFoundException("ProductStock not found.");

            await _genericRepository.DeleteAsync(productStock);
        }

        public async Task<List<ProductStockSummaryDto>> GetAllAsync()
        {
          var list = await _productStockRepository.GetAllWithIncludes();
          return _mapper.Map<List<ProductStockSummaryDto>>(list);
        }

        public async Task<ProductStockSummaryDto> GetByIdAsync(int id)
        {
           var productStock = await _productStockRepository.GetByIdWithIncludes(id);
           if(productStock == null) throw new KeyNotFoundException("ProductStock not found.");
            return _mapper.Map<ProductStockSummaryDto>(productStock);
        }
		public async Task<PaginationResult<ProductStockSummaryDto>> GetPagedAsync(PaginationRequest request)
		{
			// Get paginated entities from repository
			var pagedProductStocks = await _productStockRepository.GetPagedAsync(request);

			// Map entities → DTOs
			var dtoList = _mapper.Map<List<ProductStockSummaryDto>>(pagedProductStocks.Items);

			// Return paginated DTO result
			return new PaginationResult<ProductStockSummaryDto>(
				dtoList,
				pagedProductStocks.Page,
				pagedProductStocks.PageSize,
				pagedProductStocks.TotalRecords
			);
		}
		public async Task UpdateAsync(UpdateProductStockDto updateProductStockDto)
        {
                if (updateProductStockDto == null)
                    throw new ArgumentNullException(nameof(updateProductStockDto));

                var entity = await _genericRepository.GetByIdAsync(updateProductStockDto.Id);
                if (entity == null)
                    throw new KeyNotFoundException($"ProductStock with ID {updateProductStockDto.Id} not found.");

                var productVariantExists = await _productVariantRepository.ExistsAsync(x => x.Id == updateProductStockDto.ProductVariantId);
                if (!productVariantExists)
                    throw new KeyNotFoundException($"ProductVariant with ID {updateProductStockDto.ProductVariantId} does not exist.");

                // Map the basic properties first (exclude BlobName/ImageFile in mapping)
                _mapper.Map(updateProductStockDto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _genericRepository.UpdateAsync(entity);
        }
    }
}
