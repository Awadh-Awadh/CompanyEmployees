using Entities.DTOs;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers;

[Route("api/[controller]")]
[ApiController]


public class EmployeeController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger _logger;
    public EmployeeController(IRepositoryManager repositoryManager, ILogger logger)
    {
        _repository = repositoryManager;
        _logger = logger;
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

        var employees = _repository.Employee.GetAllEmployees(company.Id, trackChanges: false);

        return Ok(employees.Select(x => new EmployeeDTO
        {
            Id = x.Id,
            Name = x.Name,
            Position = x.Position,
            Age = x.Age
        }));
    }

    [HttpGet("{id}", Name ="GetEmployeeById")]

    public IActionResult GetCompanyEmployeeById(int companyId, int empId)
    {
        if(_repository.Company.GetCompanyById(companyId, trackChanges: false) is not { } company)
        {
            _logger.LogInformation($"company with {companyId} not found");
            return NotFound();
        }
        var employee = _repository.Employee.GetEmployee(companyId, empId, trackChanges: false);

        return Ok(new EmployeeDTO()
        {
            Id = employee.Id,
            Name = employee.Name,
            Position = employee.Position,
            Age = employee.Age
        });
    }

    [HttpPost]
    public IActionResult CreateEmployee(int companyId, [FromBody] EmployeeForCreationDTO request)
    {
        if (_repository.Company.GetCompanyById(companyId, trackChanges: false) is not { } company)
        {
            _logger.LogInformation($"Company with id {companyId} not found");
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
            return UnprocessableEntity(ModelState);
        }
        var employee = new Employee()
        {
            Age = request.Age,
            Name = request.Name,
            Position = request.Position,
        };

        _repository.Employee.CreateEmployeeForCompany(companyId, employee);

        _repository.Save();

        return CreatedAtRoute("GetEmployeeById", new { companyId , id = employee.Id}, employee);
    }

    [HttpDelete]

    public IActionResult DeleteEmployee(int companyId, int id)
    {
        if(_repository.Company.GetCompanyById(companyId, trackChanges: false) is { } )
            return NotFound();

        if( _repository.Employee.GetEmployee(companyId, id, trackChanges: false) is not { } employee)
            return NotFound( $"Employee with {id} not found");

        _repository.Employee.DeleteEmployee(employee);

        _repository.Save();

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEployeeForCompany(int companyId, int id, [FromBody] EmployeeForUpdateDTO request)
    {
        if (_repository.Company.GetCompanyById(companyId, trackChanges: false) is { })
            return NotFound();
        if(_repository.Employee.GetEmployee(companyId, id, trackChanges: true) is not { } employeeEntity)
            return NotFound();

        employeeEntity.Name = request.Name;
        employeeEntity.Age = request.Age;
        employeeEntity.Position = request.Position;

        _repository.Save();

        return NoContent();

    }

 }




