using Contracts;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository

    // UnitOfWork
    // implementation
    // specific to the application
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICompanyRepository  Company 
            {

            // repository initialization with the db context
            get

            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;  
            }
        
            }
        public IEmployeeRepository Employee 
        {
            // repository initialization with the db context
            get
            {
                if( _employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }

        public void Save() =>  _repositoryContext.SaveChanges();

        public void Dispose()        {
           _repositoryContext.Dispose();
        }
    }
}
