using Contracts;
using Entities.Model;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) :base (repositoryContext)
        {

        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int companyId, bool trackChanges) =>
             await FindByCondition(x => x.CompanyId == companyId, trackChanges)
            .OrderBy(e => e.Name).ToListAsync;
        public async Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges) =>
           await FindByCondition(x => x.CompanyId.Equals(companyId) && x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
           

        public  void CreateEmployeeForCompany(int companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

    }
}
