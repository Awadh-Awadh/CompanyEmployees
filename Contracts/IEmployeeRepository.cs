using Entities.Model;
using Entities.RequestFeatures;

namespace Contracts;

public interface IEmployeeRepository
{
    // Add methods related only to Employee only
    Task<IEnumerable<Employee>> GetAllEmployeesAsync(int companyId, EmployeeParameters employeeParameters,  bool trackChanges);
    Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges);
    void CreateEmployeeForCompany(int companyId ,Employee employee);
    void DeleteEmployee(Employee employee);

}

