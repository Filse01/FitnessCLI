using System.ComponentModel.DataAnnotations;

namespace FitnessCLI.Models;

public class Goals
{
    [Key]
    public Guid Id { get; set; }
    public int KgGoal { get; set; }
    public int CalorieGoal { get; set; }
}