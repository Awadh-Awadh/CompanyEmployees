using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        // Add methods related only to Employee only
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int companyId, bool trackChanges);
        Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges);
        void CreateEmployeeForCompany(int companyId ,Employee employee);
        void DeleteEmployee(Employee employee);

    }
   
}
