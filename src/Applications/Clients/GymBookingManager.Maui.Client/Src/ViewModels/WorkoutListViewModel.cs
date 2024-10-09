using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBookingManager.Core.Interfaces;
using GymBookingManager.Core.Services;
using GymBookingManager.Core.Utils;
using GymBookingManager.Maui.Client.Services;
using GymBookingManager.Maui.Client.Utils;

namespace GymBookingManager.Maui.Client.ViewModels;

public partial class WorkoutListViewModel : ObservableObject
{
    private readonly AppState _appState;
    private readonly BookingService _bookingService;
    private readonly IDialogService _dialogService;

    private string _searchText = string.Empty;
    
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            GetWorkouts();
        }
    }

    public WorkoutListViewModel(
        AppState appState,
        BookingService bookingService,
        IDialogService dialogService)
    {
        _appState = appState;
        _bookingService = bookingService;
        _dialogService = dialogService;
    }

    public ObservableCollection<WorkoutViewModel> Workouts { get; } = [];

    [RelayCommand]
    private async Task ShowWorkout(WorkoutViewModel workout)
    {
        await _dialogService.ShowAlertAsync(
            workout.Title,
            workout.Description,
            "OK");
    }

    [RelayCommand]
    private async Task BookWorkout(WorkoutViewModel workout)
    {
        var booked = _bookingService.BookWorkout(_appState.Username, workout.ToModel());
        if (!booked)
            return;

        _appState.CurrentUser.BookedWorkoutIds.Add(workout.Id);
        workout.IsBooked = true;
        workout.ReservedSeats += 1;
        await _dialogService.ShowSnackbarAsync($"Booked {workout.Title}");
    }

    [RelayCommand]
    private async Task CancelWorkout(WorkoutViewModel workout)
    {
        var cancelled = _bookingService.CancelWorkout(_appState.Username, workout.ToModel());
        if (!cancelled)
            return;

        _appState.CurrentUser.BookedWorkoutIds.Remove(workout.Id);
        workout.IsBooked = false;
        workout.ReservedSeats -= 1;
        await _dialogService.ShowSnackbarAsync($"Canceled {workout.Title}", isDestructive: true);
    }

    public void OnAppearing()
    {
        LoadUserBookings();
        GetWorkouts();
    }

    private void LoadUserBookings() =>
        _appState.CurrentUser.BookedWorkoutIds = _bookingService.GetUserBookings(_appState.Username);

    private void GetWorkouts()
    {
        var workouts = _bookingService.GetWorkouts();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            var viewModels = workouts
                .Select(workout => workout.ToViewModel(
                    isBooked: _appState.CurrentUser.BookedWorkoutIds.Contains(workout.Id))
                )
                .ToList();

            PopulateWorkouts(viewModels);
            return;
        }

        var filteredViewModels = workouts
            .Where(workout =>
                workout.WorkoutCategory.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                workout.StartTime.ToFriendlyDateTimeString()
                    .Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            .Select(workout => workout.ToViewModel(
                isBooked: _appState.CurrentUser.BookedWorkoutIds.Contains(workout.Id))
            )
            .ToList();

        PopulateWorkouts(filteredViewModels);
    }

    private void PopulateWorkouts(List<WorkoutViewModel> workouts)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Workouts.Clear();

            foreach (var workout in workouts)
            {
                Workouts.Add(workout);
            }
        });
    }
}