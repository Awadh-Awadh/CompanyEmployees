using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{

    // IUnitOfWork
    // interfaces Entities and complete method
    
    public interface IRepositoryManager : IDisposable
    {
        public ICompanyRepository Company { get; }
        public IEmployeeRepository Employee { get; }
        Task SaveAsync();

    }
}
