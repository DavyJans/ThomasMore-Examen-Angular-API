namespace AngularAPI.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public string Email { get; set; }
    public string Street { get; set; }

    public string City { get; set; }

    public List<Vacancy> Vacancies { get; set; }
}
