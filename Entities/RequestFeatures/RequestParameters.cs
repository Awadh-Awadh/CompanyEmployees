

namespace Entities.RequestFeatures;


/// <summary>
/// 
/// abstract to make it more generic. can be inherited by both employee and company
/// 
/// </summary>
public abstract class RequestParameters
{
    const int maxPageSize = 50;
    public int pageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}

public class EmployeeParameters : RequestParameters
{
    // uint default value is 0
    public uint MinAge { get; set; }    
    public uint MaxAge { get; set; } = int.MaxValue;   
    public bool ValidAgeRange => MaxAge > MinAge;
    public string SearchTerm { get; set; }

}
