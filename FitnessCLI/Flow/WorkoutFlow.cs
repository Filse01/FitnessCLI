using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;
using static FitnessCLI.Helpers.Helper;
namespace FitnessCLI.Flow;

public static class WorkoutFlow
{
    public async static Task ShowAddMenu()
    {
        Console.Clear();
        Console.WriteLine("1. (E)nter New Workout");
        Console.WriteLine("2. (A)dd Template");
        Console.WriteLine("3. (U)se Template");
        char inputWork = Console.ReadKey().KeyChar;
        if (inputWork == '1' || inputWork.ToString().ToLower() == "e")
        {
            await AddWorkout();
        }
        else if (inputWork == '2' || inputWork.ToString().ToLower() == "a")
        {
            await AddTemplate();
        }

        if (inputWork == '3' || inputWork.ToString().ToLower() == "u")
        {
            await UseTemplate();
        }
    }

    private static async Task UseTemplate()
    {
        List<Template> templates = new List<Template>();
        using (var context = new FitnessDbContext())
        {
            templates = await context.Templates.ToListAsync();
        }

        PrintTemplates(templates);
        Console.WriteLine("Press number to select template or any key to exit...");
        var key = Console.ReadKey();
        var intKey = int.Parse(key.KeyChar.ToString());
        Console.WriteLine();
        if (char.IsDigit(key.KeyChar))
        {
            Console.WriteLine();
            Template template = templates[intKey - 1];
            var exercises = new List<Exercise>();
            using (var context = new FitnessDbContext())
            {
                exercises = await context.Exercises.Where(e => e.TemplateId == template.Id).Select(e =>
                    new Exercise()
                    {
                        Id = Guid.NewGuid(),
                        Name = e.Name,
                        Sets = e.Sets,
                        Reps = e.Reps,
                    }).ToListAsync();
            }

            if (intKey > 0 && intKey <= templates.Count)
            {
                var workout = new Workout()
                {
                    Id = Guid.NewGuid(),
                    Name = template.Name,
                    Exercises = exercises,
                    Date = DateTime.Now
                };
                using (var context = new FitnessDbContext())
                {
                    await context.Workouts.AddAsync(workout);
                    await context.Exercises.AddRangeAsync(template.Exercises);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

    private static async Task AddTemplate()
    {
        Template template = new Template()
        {
            Id = Guid.NewGuid()
        };
        Console.Clear();
        Console.WriteLine("Enter Template Name");
        template.Name = Console.ReadLine();
        while (template.Name.Length < 1)
        {
            Console.WriteLine("Enter Template Name");
            template.Name = Console.ReadLine();
        }

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

            if (inputExercise.Length > 4)
            {
                Console.WriteLine("Please enter only Name, Sets, Reps, Kg");
                continue;
            }

            if (inputExercise.Length < 3)
            {
                Console.WriteLine("Please enter atleast Name, Sets, Reps");
                continue;
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
                Template = template,
                TemplateId = template.Id,
            };
            if (kg != 0)
            {
                exercise.Kg = kg;
            }

            exercises.Add(exercise);
        }

        using (var context = new FitnessDbContext())
        {
            template.Exercises = exercises.ToList();
            if (exercises.Count > 0)
            {
                await context.Templates.AddAsync(template);
                await context.Exercises.AddRangeAsync(exercises);
                await context.SaveChangesAsync();
            }
        }
    }

    private static async Task AddWorkout()
    {
        Workout workout = new Workout()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
        };
        Console.Clear();
        Console.WriteLine("Enter Workout Name");
        workout.Name = Console.ReadLine();
        while (workout.Name.Length < 1)
        {
            Console.WriteLine("Enter Workout Name");
            workout.Name = Console.ReadLine();
        }

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

            if (inputExercise.Length > 4)
            {
                Console.WriteLine("Please enter only Name, Sets, Reps, Kg");
                continue;
            }

            if (inputExercise.Length < 3)
            {
                Console.WriteLine("Please enter atleast Name, Sets, Reps");
                continue;
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
}