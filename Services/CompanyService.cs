namespace AngularAPI.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularAPI.Entities;
using AngularAPI.Helpers;
using AngularAPI.Models;
using AngularAPI.Data;

public interface ICompanyService
{
    IEnumerable<Company> GetAll();
    Company GetById(int id);

    Company AddCompany(Company company);

    Company UpdateCompany(Company company);

    bool DeleteCompany(Company company);
}


public class CompanyService : ICompanyService
{
    private readonly AppSettings _appSettings;
    private readonly DataContext dataContext;

    public CompanyService(IOptions<AppSettings> appSettings, DataContext dataContext)
    {
        _appSettings = appSettings.Value;
        this.dataContext = dataContext;
    }

    
    public IEnumerable<Company> GetAll()
    {
        return dataContext.Companies;
    }

    public Company GetById(int id)
    {
        return dataContext.Companies.FirstOrDefault(x => x.Id == id);
    }

    public Company AddCompany(Company company)
    {

        dataContext.Companies.Add(company);
        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        // return null if user not found
        if (company == null) return null;


        return company;
    }

    public bool DeleteCompany(Company company)
    {
        dataContext.Remove(company);
        var success = dataContext.SaveChanges();

        return success > 0;

    }

    public Company UpdateCompany(Company company)
    {
        if (company == null) return null;

        dataContext.Companies.Update(company);

        var result = dataContext.SaveChanges();

        if (result <= 0) return null;

        return company;

    }
}