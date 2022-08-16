using AngularAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI.Data;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Application> Applications { get; set; }

}
