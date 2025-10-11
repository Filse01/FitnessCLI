using System.Globalization;
using FitnessCLI.Models;

namespace FitnessCLI.Helpers;

public class Helper
{
    static public void PrintWorkout(List<Workout> list)
    {
        
        int number = 1;
        Console.WriteLine();
        if (list.Count == 0)
        {
            Console.WriteLine("You have not added any workouts");
            return;
        }
        foreach (var work in list)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Number: {number}");
            Console.WriteLine($"{work.Name} {work.Date.ToShortDateString()}:");
            foreach (var ex in work.Exercises)
            {
                Console.WriteLine($"{ex.Name},Sets: {ex.Sets},Reps: {ex.Reps}, {ex.Kg}");
            }
            Console.WriteLine("---------------------------------------------");
        
            Console.WriteLine();
            number++;
        }
    }
    static public async Task DeleteWorkout(List<Workout> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Press any key to continue...");
            var keyExit =  Console.ReadKey();
            return;
        }
        Console.WriteLine("Press d to delete or any key to exit...");
        var key = Console.ReadKey();
        Console.WriteLine();
        if (key.KeyChar == 'd')
        {
            Console.WriteLine("Choose Workout Number");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Workout workout = list[choice - 1];
            if (choice > 0 && choice <= list.Count)
            {
                using (var context = new FitnessDbContext())
                {
                    context.Workouts.Remove(workout);
                    context.Exercises.RemoveRange(workout.Exercises);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
    static public void PrintTemplates(List<Template> list)
    {
        int number = 1;
        Console.WriteLine();
        foreach (var work in list)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Number: {number}");
            Console.WriteLine($"{work.Name}");
            foreach (var ex in work.Exercises)
            {
                Console.WriteLine($"{ex.Name},Sets: {ex.Sets},Reps: {ex.Reps}, {ex.Kg}");
            }
            Console.WriteLine("---------------------------------------------");
        
            Console.WriteLine();
            number++;
        }
    }


}