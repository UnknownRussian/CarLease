using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : ControllerBase
    {
        CompanyRepo repo = new CompanyRepo();

        [HttpGet("company/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var obj = repo.GetById(id);
            if (obj == null) return NotFound($"Company with {id} not found!");
            return Ok(obj);
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var list = repo.GetAll();
            if (list.Count == 0) return NotFound("No companies available!");
            return Ok(list);
        }

        [HttpPost()]
        public IActionResult Add([FromBody] NewCompanyDTO newCompanyDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Company newCompany = new Company();
            newCompany.Id = repo.GetAll().Max(o => o.Id) + 1;
            newCompany.OwnerId = newCompanyDTO.OwnerId;
            newCompany.Name = newCompanyDTO.Name;
            newCompany.Address = newCompanyDTO.Address;
            newCompany.ZipCode = newCompanyDTO.ZipCode;
            newCompany.City = newCompanyDTO.City;
            repo.Add(newCompany);

            return Ok(new { status = "Company added!", company = newCompany });
        }

        [HttpPut()]
        public IActionResult Update([FromBody] UpdatedCompanyDTO updatedCompanyDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Company updatedCompany = new Company();
            updatedCompany.Id = updatedCompanyDTO.Id;
            updatedCompany.OwnerId = updatedCompanyDTO.OwnerId;
            updatedCompany.Name = updatedCompanyDTO.Name;
            updatedCompany.Address = updatedCompanyDTO.Address;
            updatedCompany.ZipCode = updatedCompanyDTO.ZipCode;
            updatedCompany.City = updatedCompanyDTO.City;

            repo.Update(updatedCompany);

            return Ok(new { status = $"Company with the id {updatedCompany.Id} is updated!", company = updatedCompany });
        }

        [HttpDelete("company/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (repo.GetById(id) == null) return BadRequest($"Company with the id {id} does not exist!");

            repo.Delete(id);
            return Ok($"Company with the id {id} is deleted!");
        }
    }

    public class NewCompanyDTO
    {
        public int OwnerId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(5)]
        public string ZipCode { get; set; } = string.Empty;

        [MaxLength(30)]
        public string City { get; set; } = string.Empty;
    }
    
    public class UpdatedCompanyDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(30)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(5)]
        public string ZipCode { get; set; } = string.Empty;

        [MaxLength(30)]
        public string City { get; set; } = string.Empty;
    }
}
