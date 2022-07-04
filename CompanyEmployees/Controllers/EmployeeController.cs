using Entities.DTOs;
using Entities.Model;
using Entities.RequestFeatures;
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

    public async Task<IActionResult> GetAllEmployee(int companyId, bool trackChanges, [FromQuery] EmployeeParameters employeeParameters)
    {
        var company = await _repository.Company.GetCompanyByIdAsync(companyId, trackChanges: false);
        if (company == null)
        {
            _logger.LogError("No company found");
            return NotFound();
        }

        var employees = await _repository.Employee.GetAllEmployeesAsync(companyId, employeeParameters, trackChanges: false);

        return Ok(employees.Select(x => new EmployeeDTO
        {
            Id = x.Id,
            Name = x.Name,
            Position = x.Position,
            Age = x.Age
        }));
    }

    [HttpGet("{id}", Name ="GetEmployeeById")]

    public async Task<IActionResult> GetCompanyEmployeeById(int companyId, int empId)
    {
        if(await _repository.Company.GetCompanyByIdAsync(companyId, trackChanges: false) is not { } company)
        {
            _logger.LogInformation($"company with {companyId} not found");
            return NotFound();
        }
        var employee = await _repository.Employee.GetEmployeeAsync(companyId, empId, trackChanges: false);

        return Ok(new EmployeeDTO()
        {
            Id = employee.Id,
            Name = employee.Name,
            Position = employee.Position,
            Age = employee.Age
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(int companyId, [FromBody] EmployeeForCreationDTO request)
    {
        if (await _repository.Company.GetCompanyByIdAsync(companyId, trackChanges: false) is not { } company)
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

        await _repository.SaveAsync();

        return CreatedAtRoute("GetEmployeeById", new { companyId , id = employee.Id}, employee);
    }

    [HttpDelete]

    public async Task<IActionResult> DeleteEmployee(int companyId, int id)
    {
        if(await _repository.Company.GetCompanyByIdAsync(companyId, trackChanges: false) is { } )
            return NotFound();

        if( await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges: false) is not { } employee)
            return NotFound( $"Employee with {id} not found");

        _repository.Employee.DeleteEmployee(employee);

       await _repository.SaveAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEployeeForCompany(int companyId, int id, [FromBody] EmployeeForUpdateDTO request)
    {
        if (await _repository.Company.GetCompanyByIdAsync(companyId, trackChanges: false) is { })
            return NotFound();
        if(await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges: true) is not { } employeeEntity)
            return NotFound();

        employeeEntity.Name = request.Name;
        employeeEntity.Age = request.Age;
        employeeEntity.Position = request.Position;

       await _repository.SaveAsync();

        return NoContent();

    }

 }




