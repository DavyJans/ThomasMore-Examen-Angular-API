namespace AngularAPI.Services;

using Microsoft.Extensions.Options;
using AngularAPI.Entities;
using AngularAPI.Helpers;
using AngularAPI.Data;
using Microsoft.EntityFrameworkCore;

public interface IVacancyService
{
    IEnumerable<Vacancy> GetAll();
    Vacancy GetById(int id);
}

public class VacancyService : IVacancyService
{
   
    private readonly AppSettings _appSettings;
    private readonly DataContext dataContext;

    public VacancyService(IOptions<AppSettings> appSettings, DataContext dataContext)
    {
        _appSettings = appSettings.Value;
        this.dataContext = dataContext;
    }

 
    public IEnumerable<Vacancy> GetAll()
    {
        return dataContext.Vacancies.Include(x => x.Company);
    }

    public Vacancy GetById(int id)
    {
        return dataContext.Vacancies.FirstOrDefault(x => x.Id == id);
    }


}