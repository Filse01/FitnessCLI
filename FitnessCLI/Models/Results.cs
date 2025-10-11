using System.ComponentModel.DataAnnotations;

namespace FitnessCLI.Models;

public class Results
{
    [Key]
    public Guid Id { get; set; }
    public int Weight { get; set; }
    public int Calories { get; set; }
    public DateTime Date { get; set; }
}