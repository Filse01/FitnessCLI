using System.ComponentModel.DataAnnotations;

namespace FitnessCLI.Models;

public class Workout
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
    
}