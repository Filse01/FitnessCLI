using System.Globalization;
using FitnessCLI;
using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;

while (true)
{ 
Console.Clear();
Console.WriteLine("1. (A)dd Workout");
Console.WriteLine("2. View (P)ast Workouts");

char input = Console.ReadKey().KeyChar;
if (input == '1' || input.ToString().ToLower() == "a")
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
    
}

if (input == '2' || input.ToString().ToLower() == "p")
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
        PrintWorkout(workouts);
        Console.WriteLine("Press d to delete or any key to exit...");
        var key = Console.ReadKey();
        if (key.KeyChar == 'd')
        {
            Console.WriteLine("Choose Workout Number");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Workout workout = workouts[choice];
            if (choice > 0 && choice <= workouts.Count)
            {
                using (var context = new FitnessDbContext())
                {
                    context.Workouts.Remove(workouts[choice]);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

    if (inputHist == '2' || inputHist.ToString().ToLower() == "l")
    {
        List<Workout> last10work = workouts.OrderByDescending(w => w.Date).Take(10).ToList();
        PrintWorkout(last10work);
    }

    if (inputHist == '3' || inputHist.ToString().ToLower() == "e")
    {
        Console.WriteLine("Enter Date Format: mm/dd");
        var workoutDate = DateTime.Parse(Console.ReadLine());
        List<Workout> workDate = workouts.Where(w => w.Date.Day == workoutDate.Day).ToList();
        PrintWorkout(workDate);
    }
}
}

void PrintWorkout(List<Workout> list)
{
    int number = 1;
    Console.WriteLine();
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