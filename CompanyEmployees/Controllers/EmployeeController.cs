using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        public EmployeeController(IRepositoryManager repositoryManager, ILogger logger)
        {
            repositoryManager = _repository;
            logger = _logger;
        }

        [HttpGet]

        public IActionResult GetAllEmployee(int companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompanyById(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogError("No company found");
                return NotFound();  
            }

            var employees = _repository.Employee.GetAllEmployees(company.Id , trackChanges: false);
            return Ok(employees.Select(x => new EmployeeDTO
            {
                Id = x.Id,
                Name = x.Name,
                Position = x.Position,
                Age = x.Age
            }));
        }
    }

    
}
