using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessCLI.Flow;
using static FitnessCLI.Helpers.Helper;
public static class HistoryFlow
{
    public async static Task ShowHistoryView()
    {
        Console.Clear();
        Console.WriteLine("1. Show (A)ll");
        Console.WriteLine("2. Show (l)ast 10");
        Console.WriteLine("3. (E)xact Date");
        char inputHist = Console.ReadKey().KeyChar;
        List<Workout> workouts = new List<Workout>();
        using (var context = new FitnessDbContext())
        {
            workouts = await context.Workouts.Include(w => w.Exercises).ToListAsync();
        }
        if (inputHist == '1' || inputHist.ToString().ToLower() == "a")
        {
            await ShowAll(workouts);
        }

        if (inputHist == '2' || inputHist.ToString().ToLower() == "l")
        {
            await ShowLast10(workouts);
        }

        if (inputHist == '3' || inputHist.ToString().ToLower() == "e")
        {
            await ExactDate(workouts);
        }
    }

    private static async Task ExactDate(List<Workout> workouts)
    {
        Console.WriteLine("Enter Date Format: mm/dd");
        var workoutDate = DateTime.Parse(Console.ReadLine());
        List<Workout> workDate = workouts.Where(w => w.Date.Day == workoutDate.Day).ToList();
        PrintWorkout(workDate);
        await DeleteWorkout(workouts);
    }

    private static async Task ShowLast10(List<Workout> workouts)
    {
        List<Workout> last10work = workouts.OrderByDescending(w => w.Date).Take(10).ToList();
        PrintWorkout(last10work);
        await DeleteWorkout(workouts);
    }

    private static async Task ShowAll(List<Workout> workouts)
    {
        PrintWorkout(workouts);
        await DeleteWorkout(workouts);
    }
}