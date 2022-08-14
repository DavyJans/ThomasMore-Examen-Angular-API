using System.Text.Json.Serialization;

namespace AngularAPI.Entities;

public class Vacancy
{
    public int Id { get; set; }
    public string FunctionTitle { get; set; }
    public string JobDescriptionTitle { get; set; }
    public string JobDescriptionContent { get; set; }
    public string ProfileContent { get; set; }
    public string OfferContent { get; set; }
    public string OtherContent { get; set; }
    public string PublishDate { get; set; }
    public string Editor { get; set; }
    public string Author { get; set; }

    public int CompanyId { get; set; }
    
    public virtual Company? Company { get; set; }
}
