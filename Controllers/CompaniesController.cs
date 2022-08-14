namespace AngularAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Services;
using AngularAPI.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

[ApiController]
[Route("[controller]")]
public class CompaniesController : ControllerBase
{
    private ICompanyService companyService;

    public CompaniesController(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    [Authorize]
    [HttpPost("add")]
    public IActionResult Add(Company company)
    {
        var response = companyService.AddCompany(company);

        if (response == null)
            return BadRequest(new { message = "Error during add operation" });

        return Ok(response);
    }

    [Authorize]
    [HttpDelete("delete")]
    public IActionResult Delete(Company company)
    {
        var response = companyService.DeleteCompany(company);

        if (!response)
            return BadRequest(new { message = "Error during delete operation" });

        return Ok($"Company {company.Name} successfully deleted");
    }

    [Authorize]
    [HttpPut("update")]
    public IActionResult Update(Company Company)
    {
        var response = companyService.UpdateCompany(Company);

        if (response == null)
            return BadRequest(new { message = "Error during update operation" });

        return Ok($"Company {Company.Name} successfully updated");

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var companies = companyService.GetAll();
        return Ok(companies);
    }
}