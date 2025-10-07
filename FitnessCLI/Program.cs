using System.Globalization;
using FitnessCLI;
using FitnessCLI.Flow;
using FitnessCLI.Models;
using Microsoft.EntityFrameworkCore;
while (true)
{ 
Console.Clear();
Console.WriteLine("1. (A)dd New Workout");
Console.WriteLine("2. View (P)ast Workouts");
Console.WriteLine("3. (S)tats");
char input = Console.ReadKey().KeyChar;
if (input == '1' || input.ToString().ToLower() == "a")
{
    await WorkoutFlow.ShowAddMenu();
}
else if (input == '2' || input.ToString().ToLower() == "p")
{
    await HistoryFlow.ShowHistoryView();
}
else if (input == '3' || input.ToString().ToLower() == "s")
{
    await StatsFlow.ShowStatsMenu();
}
}