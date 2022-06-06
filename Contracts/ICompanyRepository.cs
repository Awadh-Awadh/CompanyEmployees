using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        // Add methods related only to Company only

        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompanyById(int id, bool trackChanges);
    }
}
