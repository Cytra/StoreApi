namespace Application.Models.Pagination;

public class PagedList<T>
{
    public IEnumerable<T> Items { get; set; }
    public PagingModel Paging { get; set; }
}

public class PagingModel
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (TotalItems + PageSize - 1) / PageSize;
}