using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessCLI.Flow;

public static class GoalsFlow
{
    public async static Task ShowGoalsView()
    {
        Console.Clear();
        Console.WriteLine("1. Enter Today's (D)ata");
        Console.WriteLine("2. (S)how Goals");
        Console.WriteLine("3. (E)nter Goals");
        char goalsInput = Console.ReadKey().KeyChar;
        List<Goals> workouts = new List<Goals>();
        using (var context = new FitnessDbContext())
        {
            workouts = await context.Goals.ToListAsync();
        }
        if (goalsInput == '3' || goalsInput.ToString().ToLower() == "e")
        {
            await EnterGoals();
        }
        
        if (goalsInput == '2' || goalsInput.ToString().ToLower() == "s")
        {
            await ShowGoals();
        }
        if (goalsInput == '1' || goalsInput.ToString().ToLower() == "d")
        {
            await TodayData();
        }
    }

    private static async Task TodayData()
    {
        Console.WriteLine();
        Console.WriteLine("Enter today's weight: ");
        int weightGoal = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter today's calories: ");
        int calorieGoal = int.Parse(Console.ReadLine());
        var Result = new Results()
        {
            Id = Guid.NewGuid(),
            Calories = calorieGoal,
            Weight = weightGoal,
            Date = DateTime.Now,
        };
        using (var context = new FitnessDbContext())
        {
            await context.Results.AddAsync(Result);
            await context.SaveChangesAsync();
        }
    }
    private static async Task EnterGoals()
    {
        Console.WriteLine();
        Console.WriteLine("Enter desired weight goal: ");
        int weightGoal = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter desired calorie goal: ");
        int calorieGoal = int.Parse(Console.ReadLine());
        var Goals = new Goals()
        {
            Id = Guid.NewGuid(),
            KgGoal = weightGoal,
            CalorieGoal = calorieGoal,
        };
        using (var context = new FitnessDbContext())
        {
            await context.Goals.AddAsync(Goals);
            await context.SaveChangesAsync();
        }
    }

    private static async Task ShowGoals()
    {
        Console.WriteLine();
        var results = new Results();
        var goals = new Goals();
        using (var context = new FitnessDbContext())
        {
            results = await context.Results.FirstOrDefaultAsync(x => x.Date.Day == DateTime.Now.Day);
            goals = await context.Goals.FirstOrDefaultAsync();
        }
        Console.WriteLine("Today's Results: ");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Weight: {results.Weight}/{goals.KgGoal}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Calories: {results.Calories}/{goals.CalorieGoal}");
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Press any key to exit...");
        var key = Console.ReadKey();
    }
}