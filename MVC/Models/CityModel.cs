namespace MVC.Models;

using System.ComponentModel.DataAnnotations;

public class CityModel
{
    // Åtkomst till städers lagrade data samt valideringsmeddelanden

    [Required(ErrorMessage = "Fältet är ej korrekt ifyllt. Försök igen.")]
    [Display(Name="Stadens namn")]
    public string? name { get; set; }

    [Required(ErrorMessage = "Fältet är ej korrekt ifyllt. Försök igen.")]
    [Display(Name="Stadens landskap")]
    public string? county { get; set; }
}