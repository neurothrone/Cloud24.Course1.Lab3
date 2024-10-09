using GymBookingManager.Maui.Client.ViewModels;
using GymBookingManager.Maui.Client.Views.Pages;

namespace GymBookingManager.Maui.Client;

public partial class App : Application
{
    private readonly AuthViewModel _authViewModel;

    public App(AuthViewModel authViewModel)
    {
        InitializeComponent();

        _authViewModel = authViewModel;
        _authViewModel.AuthStateChanged += OnAuthStateChanged;
        OnAuthStateChanged(_authViewModel.IsAuthenticated);
    }

    private void OnAuthStateChanged(bool isAuthenticated)
    {
        MainPage = isAuthenticated ? new AppShell() : new AuthPage(_authViewModel);
    }
}