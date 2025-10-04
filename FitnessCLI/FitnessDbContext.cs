using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessCLI;

public class FitnessDbContext : DbContext
{
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=FitnessCli; User Id = sa; Password=Mnogodebel_123; Encrypt=False;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;");
    }
}