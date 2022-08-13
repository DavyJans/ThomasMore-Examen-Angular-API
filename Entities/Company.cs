namespace AngularAPI.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Vacancy> Vacancies { get; set; }
}
