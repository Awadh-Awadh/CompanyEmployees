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

        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyByIdAsync(int id, bool trackChanges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetCompanyCollectionAsync(IEnumerable<int> id, bool trackChanges);
        void DeleteCompany(Company company);
    }
}
