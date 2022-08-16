namespace AngularAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Services;
using AngularAPI.Entities;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
public class VacanciesController : ControllerBase
{
    private IVacancyService vacancyService;

    public VacanciesController(IVacancyService vacancyService)
    {
        this.vacancyService = vacancyService;
    }

    [Authorize]
    [HttpPost("add")]
    public IActionResult Add(Vacancy vacancy)
    {
        var response = vacancyService.AddVacancy(vacancy);

        if (response == null)
            return BadRequest(new { message = "Error during add operation" });

        return Ok(response);
    }

    [Authorize]
    [HttpDelete("delete")]
    public IActionResult Delete(Vacancy vacancy)
    {
        var response = vacancyService.DeleteVacancy(vacancy);

        if (!response)
            return BadRequest(new { message = "Error during delete operation" });

        return Ok($"Vacancy {vacancy.FunctionTitle} successfully deleted");
    }

    [Authorize]
    [HttpPut("update")]
    public IActionResult Update(Vacancy vacancy)
    {
        var response = vacancyService.UpdateVacancy(vacancy);

        if (response == null)
            return BadRequest(new { message = "Error during update operation" });

        return Ok($"Vacancy {vacancy.FunctionTitle} successfully updated");

    }


    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var vacancy = vacancyService.GetById(id);
        return Ok(vacancy);
    }


    [HttpPost("apply")]
    public IActionResult Apply([FromBody] JsonElement body)
    {
        ApplicationRequest applicationRequest = JsonSerializer.Deserialize<ApplicationRequest>(body, new JsonSerializerOptions
                                                                    {
                                                                        PropertyNameCaseInsensitive = true
                                                                    });
     

        var response = vacancyService.Apply(applicationRequest.user, applicationRequest.id);

        if (response == null)
            return BadRequest(new { message = "Error during application" });

        return Ok(response);
    }


    [HttpGet("byCompanyId")]
    public IActionResult getVacanciesByCompanyId(int id)
    {
        var vacancies = vacancyService.GetByCompanyId(id);
        return Ok(vacancies);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var vacancies = vacancyService.GetAll();
        return Ok(vacancies);
    }
}