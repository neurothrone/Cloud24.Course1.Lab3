using GymBookingManager.Core.Models;
using GymBookingManager.Maui.Client.ViewModels;

namespace GymBookingManager.Maui.Client.Utils;

public static class WorkoutExtensions
{
    public static WorkoutViewModel ToViewModel(this Workout workout, bool isBooked = false) => new()
    {
        Id = workout.Id,
        Title = workout.Title,
        Description = workout.Description,
        WorkoutCategory = workout.WorkoutCategory,
        StartTime = workout.StartTime,
        Duration = workout.Duration,
        MaxSeats = workout.MaxSeats,
        ReservedSeats = workout.ReservedSeats,
        IsBooked = isBooked,
    };

    public static Workout ToModel(this WorkoutViewModel viewModel) => new()
    {
        Id = viewModel.Id,
        Title = viewModel.Title,
        Description = viewModel.Description,
        WorkoutCategory = viewModel.WorkoutCategory,
        StartTime = viewModel.StartTime,
        Duration = viewModel.Duration,
        MaxSeats = viewModel.MaxSeats,
        ReservedSeats = viewModel.ReservedSeats
    };
}