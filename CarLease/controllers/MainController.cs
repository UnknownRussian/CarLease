using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/main")]
    public class ItemsController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            CompanyRepo companyRepo = new CompanyRepo();
            companyRepo.GetAllCompanies().ForEach(c => Console.WriteLine($"Company Name: {c.Name}"));
            Company? company = companyRepo.GetCompanyById(2);
            if (company != null) Console.WriteLine($"Id: {company.Id}, Company name: {company.Name}");
            return Ok();
        }
    }
}
