namespace AngularAPI.Services;

using Microsoft.Extensions.Options;
using AngularAPI.Entities;
using AngularAPI.Helpers;
using AngularAPI.Data;
using Microsoft.EntityFrameworkCore;
using AngularAPI.Models;

public interface IVacancyService
{
    IEnumerable<Vacancy> GetAll();
    Vacancy GetById(int id);
    Vacancy AddVacancy(Vacancy vacancy);

    Vacancy UpdateVacancy(Vacancy vacancy);

    bool DeleteVacancy(Vacancy vacancy);

    IEnumerable<Vacancy> GetByCompanyId(int id);

    User Apply(User user, int vacancyId);
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

    public User Apply(User user, int vacancyId)
    {
        if (vacancyId == null || user.Id == null) return null;

        Application application = new()
        {
            ApplicationDate = DateTime.Now.ToShortDateString(),
            UserId = user.Id,
            VacancyId = vacancyId

        };

        dataContext.Applications.Add(application);

        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        return user;

    }


    public IEnumerable<Vacancy> GetAll()
    {
        return dataContext.Vacancies.Include(x => x.Company);
    }

    public Vacancy GetById(int id)
    {
        return dataContext.Vacancies.Include(x => x.Company).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Vacancy> GetByCompanyId(int id)
    {
        return dataContext.Vacancies.Include(x => x.Company).Where(x => x.CompanyId == id);
    }

    public Vacancy AddVacancy(Vacancy vacancy)
    {
        
        dataContext.Vacancies.Add(vacancy);
        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        // return null if user not found
        if (vacancy == null) return null;

     
        return vacancy;
    }

    public bool DeleteVacancy(Vacancy vacancy)
    {
        dataContext.Remove(vacancy);
        var success = dataContext.SaveChanges();

        return success > 0;

    }

    public Vacancy UpdateVacancy(Vacancy vacancy)
    {
        if (vacancy == null) return null;

        dataContext.Vacancies.Update(vacancy);

        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        return vacancy;

    }
}