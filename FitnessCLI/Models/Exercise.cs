using System.ComponentModel.DataAnnotations;

namespace FitnessCLI.Models;

public class Exercise
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)] 
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(2)]
    public int Sets { get; set; }
    [Required]
    [MaxLength(2)]
    public int Reps { get; set; }
    public int? Kg  { get; set; }
    public Guid WorkouId { get; set; }
    public virtual Workout Workout { get; set; }
}