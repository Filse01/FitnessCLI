using FitnessCLI;
using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;

while (true)
{
Console.WriteLine("1. Add Workout(A)");
Console.WriteLine("2. View Past Workouts(B)");

string input = Console.ReadLine();
if (input == "1" || input.ToLower() == "a")
{
    Workout workout = new Workout()
    {
        Id = Guid.NewGuid(),
        Date = DateTime.Now,
    };
    Console.Clear();
    Console.WriteLine("Enter Workout Name");
    workout.Name = Console.ReadLine();
    Console.WriteLine("Type: Name, Sets, Reps, Kg");
    Console.WriteLine("When ready type \"Done\"");
    List<Exercise> exercises = new List<Exercise>();
    
    while (true)
    {
        string[] inputExercise = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
        if (inputExercise[0].ToLower() == "done")
        {
            break;
        }

        string exName = inputExercise[0];
        int exSets = int.Parse(inputExercise[1]);
        int exReps = int.Parse(inputExercise[2]);
        int kg = 0;
        if (inputExercise.Length == 4)
        {
            kg = int.Parse(inputExercise[3]);
        }

        Exercise exercise = new Exercise()
        {
            Name = exName,
            Sets = exSets,
            Reps = exReps,
            Workout = workout,
            WorkouId = workout.Id,
        };
        if (kg != 0)
        {
            exercise.Kg = kg;
        }
        exercises.Add(exercise);
        
    }
    using (var context = new FitnessDbContext())
    {
        workout.Exercises = exercises.ToList();
        if (exercises.Count > 0)
        {
            await context.Workouts.AddAsync(workout);
            await context.Exercises.AddRangeAsync(exercises);
            await context.SaveChangesAsync();
        }
           
    }
    Console.Clear();
}

if (input == "2" || input.ToLower() == "b")
{
    Console.Clear();
    List<Workout> wokrouts = new List<Workout>();
    using (var context = new FitnessDbContext())
    {
        wokrouts = await context.Workouts.Include(w => w.Exercises).ToListAsync();
    }

    foreach (var work in wokrouts)
    {
        Console.WriteLine($"{work.Name} {work.Date.ToShortDateString()}:");
        foreach (var ex in work.Exercises)
        {
            Console.WriteLine($"{ex.Name},Sets: {ex.Sets},Reps: {ex.Reps}, {ex.Kg}");
        }
        Console.WriteLine();
    }
}
}