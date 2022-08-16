namespace AngularAPI.Entities;

public class Application
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VacancyId { get; set; }

    public string ApplicationDate { get; set; }

    public User User { get; set; }
    public Vacancy Vacancy { get; set; }
}
