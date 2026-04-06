using Microsoft.EntityFrameworkCore;
using Nois.Domain.Common;

namespace Nois.Application.Extensions
{
	public static class PaginationExtensions
	{
		public static async Task<PaginationResult<T>> ToPaginatedResultAsync<T>(
			this IQueryable<T> query,
			int pageNumber,
			int pageSize)
		{
			var totalRecords = await query.CountAsync();

			var data = await query
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PaginationResult<T>(
				data,
				pageNumber,
				pageSize,
				totalRecords
			);
		}
		public static async Task<PaginationResult<T>> ToPaginatedResultAsync<T>(this IQueryable<T> query,PaginationRequest request)
		{
			var totalRecords = await query.CountAsync();

			var data = await query
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync();

			return new PaginationResult<T>(
				data,
				request.Page,
				request.PageSize,
				totalRecords
			);
		}

	}
}
