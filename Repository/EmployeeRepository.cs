using Contracts;
using Entities.Model;
using Entities.RequestFeatures;
using Infra.Data;
using Repository.Extensions;



namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) :base (repositoryContext)
    {

    }

    public async Task<PagedList<Employee>> GetAllEmployeesAsync(int companyId, EmployeeParameters employeeParameters, bool trackChanges)
    {
        var employees = await FindByCondition(e => e.CompanyId == companyId, trackChanges)
            .Filter(employeeParameters.MinAge, employeeParameters.MaxAge)
            .Search(employeeParameters.SearchTerm)
            .OrderBy(e =>e.Name)
            .ToListAsync();

        return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageSize, employeeParameters.PageSize);
    }
    public async Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges) =>
       await FindByCondition(x => x.CompanyId.Equals(companyId) && x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
       

    public  void CreateEmployeeForCompany(int companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }

    public void DeleteEmployee(Employee employee) => Delete(employee);

}
