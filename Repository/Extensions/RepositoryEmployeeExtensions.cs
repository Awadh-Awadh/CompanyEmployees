using System.Reflection;
using System.Text;
using Entities.Model;
using System.Linq.Dynamic.Core;


namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return employees.OrderBy(e => e.Name);
        }

        var orderParams = orderByQueryString.Trim().Split(',');
        
        var propertyIfos = typeof(Employee).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        var orderQueryBuilder = new StringBuilder();
        
        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;
            
            var propertyFromQueryName = param.Split(" ")[0];
            
            var objectProperty = propertyIfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName,
                StringComparison.InvariantCultureIgnoreCase));
            
            if(objectProperty is null)
                continue;
            
            var direction = param.EndsWith("desc") ? "descending" : "ascending";
            
            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}");
        }
        
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        
        return string.IsNullOrWhiteSpace(orderQuery) ? employees.OrderBy(e => e.Name) : employees.OrderBy(orderQuery);
    }

    public static IQueryable<Employee> Filter(this IQueryable<Employee> employees, uint minAge, uint maxAge)
    {
        return employees.Where(e => e.Age < minAge && e.Age > maxAge);
    }
    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return employees;
        }
        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }
}
