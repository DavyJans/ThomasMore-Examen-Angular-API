namespace AngularAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Services;

[ApiController]
[Route("[controller]")]
public class VacanciesController : ControllerBase
{
    private IVacancyService vacancyService;

    public VacanciesController(IVacancyService vacancyService)
    {
        this.vacancyService = vacancyService;
    }


    //[Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var vacancies = vacancyService.GetAll();
        return Ok(vacancies);
    }
}