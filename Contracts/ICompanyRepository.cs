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
        void CreateCompany(Company company);
        IEnumerable<Company> GetCompanyCollection(IEnumerable<int> id, bool trackChanges);
        void DeleteCompany(Company company);
    }
}
