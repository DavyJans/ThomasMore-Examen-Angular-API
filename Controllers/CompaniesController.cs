namespace AngularAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Services;

[ApiController]
[Route("[controller]")]
public class CompaniesController : ControllerBase
{
    private ICompanyService companyService;

    public CompaniesController(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    

    [HttpGet]
    public IActionResult GetAll()
    {
        var companies = companyService.GetAll();
        return Ok(companies);
    }
}