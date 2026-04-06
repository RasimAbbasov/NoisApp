namespace Nois.Domain.Common
{
	public record PaginationRequest
	{
		private int _pageSize = 10; 
		private int _page = 1;
		private const int MaxPageSize = 50; // Prevent over-fetching

		public int Page
		{
			get => _page;
			set => _page = value < 1 ? 1 : value; 
		}
		public int PageSize
		{
			get => _pageSize;
			init => _pageSize = value > MaxPageSize ? MaxPageSize : value;
		}
	}
}