using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;

        public CompaniesController(IRepositoryManager repositoryManager, ILogger logger)
        {
            repositoryManager = _repository;
            logger = _logger;
        }

        [HttpGet]

        public IActionResult GetCompanies()
        {
            try
            {
                var comapnies = _repository.Company.GetAllCompanies(trackChanges: false);
                var companyDto = comapnies.Select(x => new CompanyDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    FullAddress = string.Join(' ', x.Address, x.Country)
                }).ToList();

                return Ok(companyDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal Server error");
            }
        }


        [HttpGet("{id}")]

        public ActionResult GetCompanyById(int id)
        {
           var company = _repository.Company.GetCompanyById(id, trackChanges: false);
           if(company == null)
            {
                _logger.LogError($"Company with {id} not found");
            }
             var companyDto = new CompanyDTO
                {
                    Id = company.Id,
                    Name = company.Name,
                    FullAddress = string.Join(' ', company.Address, company.Country)
                };

                return Ok(companyDto);
        }
        


    }
}
