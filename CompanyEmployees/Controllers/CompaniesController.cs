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

                return Ok(comapnies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal Server error");
            }
        }




    }
}
