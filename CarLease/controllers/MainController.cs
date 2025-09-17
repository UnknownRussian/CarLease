using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/main")]
    public class MainController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            Company newCompany = new Company();
            newCompany.Id = 3;
            newCompany.OwnerId = 2;
            newCompany.Name = "ScrapCars";
            newCompany.Address = "Something Vej 31";
            newCompany.ZipCode = "4000";
            newCompany.City = "Roskilde";

            CompanyRepo companyRepo = new CompanyRepo();
            companyRepo.Add(newCompany);
            companyRepo.GetAll().ForEach(c => Console.WriteLine($"Company Name: {c.Name}"));
            Company? company = companyRepo.GetById(2);
            if (company != null) Console.WriteLine($"Id: {company.Id}, Company name: {company.Name}");


            return Ok();
        }

        [HttpGet("test2")]
        public IActionResult Test2()
        {
            VehicleRepo vehicleRepo = new VehicleRepo();
            vehicleRepo.GetAll().ForEach(v => Console.WriteLine($"{v.Manufacture} {v.Model}"));

            CompanyRepo companyRepo = new CompanyRepo();
            // Company company = companyRepo.GetById(3);
            // company.Address = "New Vej 42";
            // companyRepo.Update(company);
            companyRepo.Delete(3);

            return Ok();
        }
    }
}
