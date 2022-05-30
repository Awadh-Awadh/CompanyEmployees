using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : Repository<Company>, ICompanyRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) :base(repositoryContext)
        {

        }
    }
}
