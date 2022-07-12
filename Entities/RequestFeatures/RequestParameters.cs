using System.Linq.Dynamic.Core;

namespace Entities.RequestFeatures;


/// <summary>
/// 
/// abstract to make it more generic. can be inherited by both employee and company
/// 
/// </summary>
public abstract class RequestParameters
{
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize {
        get => _pageSize;
       
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
        
    }
}

public class EmployeeParameters : RequestParameters
{
    // uint default value is 0
    public EmployeeParameters()
    {
        OrderBy = "name";
    }
    public uint MinAge { get; set; }    
    public uint MaxAge { get; set; } = int.MaxValue;   
    public bool ValidAgeRange => MaxAge > MinAge;
    public string SearchTerm { get; set; } = default!; 
    public string OrderBy { get; set; } 

}
