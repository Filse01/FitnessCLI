using System.ComponentModel.DataAnnotations;

namespace FitnessCLI.Models;

public class Template
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
}