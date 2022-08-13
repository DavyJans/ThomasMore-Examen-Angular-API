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

   
}