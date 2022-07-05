
namespace Entities.RequestFeatures;

public class MetaData
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevioud => CurrentPage > 1;
    public bool HasNextioud => CurrentPage < TotalPages;
}
