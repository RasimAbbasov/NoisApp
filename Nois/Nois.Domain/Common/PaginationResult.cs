namespace Nois.Domain.Common
{
    public class PaginationResult<T>
    {
		public IEnumerable<T> Items { get; private set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalRecords { get; set; }
		public int TotalPages { get; set; }
		public bool HasPrevious => Page > 1;
		public bool HasNext => Page < TotalPages;
		public PaginationResult(IEnumerable<T> items, int page, int pageSize, int totalRecords)
		{
			Items = items;
			Page = page;
			PageSize = pageSize;
			TotalRecords = totalRecords;
			TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
		}
	}
}
