namespace FitnessCLI.Models;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int? Kg  { get; set; }
    public Guid WorkouId { get; set; }
    public virtual Workout Workout { get; set; }
}