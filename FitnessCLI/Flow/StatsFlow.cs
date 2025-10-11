using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessCLI.Flow;

public class StatsFlow
{
    public static async Task ShowStatsMenu()
    {
        Console.Clear();
        List<Workout> workouts = new List<Workout>();
        using (var context = new FitnessDbContext())
        {
            workouts = await context.Workouts.Include(w => w.Exercises).ToListAsync();
        }

        if (workouts.Count > 0)
        {
            Console.WriteLine($"Total Workouts: {workouts.Count}");
            int sets = 0;
            foreach (var w in workouts)
            {
                foreach (var ex in w.Exercises)
                {
                    sets += ex.Sets;
                }
            }
            Console.WriteLine($"Total Sets: {sets}");
            int reps = 0;
            foreach (var w in workouts)
            {
                foreach (var ex in w.Exercises)
                {
                    reps += ex.Sets * ex.Reps;
                }
            }

            Console.WriteLine($"Total Reps: {reps}");
        }
        Console.WriteLine("Press any key to exit...");
        var key = Console.ReadKey();
    }
}