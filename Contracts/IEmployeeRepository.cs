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
        IEnumerable<Employee> GetAllEmployee(int companyId, bool trackChanges);
        Employee GetEmployee(int companyId, int id, bool trackChanges);
    }
   
}
