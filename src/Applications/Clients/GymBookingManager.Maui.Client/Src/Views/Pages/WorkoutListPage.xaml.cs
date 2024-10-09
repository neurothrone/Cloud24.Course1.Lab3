using GymBookingManager.Maui.Client.ViewModels;

namespace GymBookingManager.Maui.Client.Views.Pages;

public partial class WorkoutListPage : ContentPage
{
    private readonly AuthViewModel _authViewModel;

    public WorkoutListPage(
        WorkoutListViewModel workoutListViewModel,
        AuthViewModel authViewModel)
    {
        InitializeComponent();
        BindingContext = workoutListViewModel;
        _authViewModel = authViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is WorkoutListViewModel viewModel)
            viewModel.OnAppearing();
    }

    private void OnLogOutClicked(object? sender, EventArgs e)
    {
        _authViewModel.LogOut();
    }
}