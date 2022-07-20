using Entities.DTOs;
using Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/Companies")]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;

        public CompaniesController(IRepositoryManager repositoryManager, ILogger logger)
        {
            _repository = repositoryManager ;
            _logger = logger;
        }

        [HttpGet]

        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var comapnies = await _repository.Company.GetAllCompaniesAsync(trackChanges: false);
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


        [HttpGet("{id}", Name = "CompanyById")]
        [ResponseCache(Duration = 180)]

        public async Task<ActionResult> GetCompanyById(int id)
        {
           var company = await _repository.Company.GetCompanyByIdAsync(id, trackChanges: false);

           if(company == null)
                _logger.LogError($"Company with {id} not found");            

           var companyDto = new CompanyDTO
                {
                    Id = company.Id,
                    Name = company.Name,
                    FullAddress = string.Join(' ', company.Address, company.Country)
                };

                return Ok(companyDto);
        }

        [HttpGet("collection/{ids}", Name = "CompanyCollection")]

        public async Task<IActionResult> GetCompanyCollection(IEnumerable<int> ids)
        {
            var companies = await _repository.Company.GetCompanyCollectionAsync(ids, trackChanges: false);
            if (companies.Count() != ids.Count())
                return NotFound($"some ids are not valid");
            foreach(var company in companies)
            {
                return Ok(new CompanyDTO()
                {
                    Id=company.Id,
                    Name=company.Name,
                    FullAddress=company.Address,

                });
            }
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyForCreationDto company)
        {
            if(company == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Company ForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var companyEntity = new Company()
            {
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };
            
            _repository.Company.CreateCompany(companyEntity);
            
            await _repository.SaveAsync();

            return CreatedAtRoute("CompanyById", new { id = companyEntity.Id }, companyEntity);
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (await _repository.Company.GetCompanyByIdAsync(id, trackChanges: false) is not { } company)
            {
                _logger.LogInformation($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Company.DeleteCompany(company);

            await _repository.SaveAsync();

            return NoContent();
        }
        


    }
}
