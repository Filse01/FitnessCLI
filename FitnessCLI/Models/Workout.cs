namespace FitnessCLI.Models;

public class Workout
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
    
}