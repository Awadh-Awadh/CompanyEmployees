using Contracts;
using Entities.Model;
using Entities.RequestFeatures;
using Infra.Data;


namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) :base (repositoryContext)
    {

    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int companyId, EmployeeParameters employeeParameters, bool trackChanges) =>
         FindByCondition(x => x.CompanyId == companyId, trackChanges)
        .OrderBy(e => e.Name)
        .Skip((employeeParameters.pageNumber - 1) * employeeParameters.PageSize)
        .Take(employeeParameters.PageSize)
        .ToList();
    public async Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges) =>
       await FindByCondition(x => x.CompanyId.Equals(companyId) && x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
       

    public  void CreateEmployeeForCompany(int companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }

    public void DeleteEmployee(Employee employee) => Delete(employee);

}
