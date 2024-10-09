using GymBookingManager.Maui.Client.ViewModels;

namespace GymBookingManager.Maui.Client.Views.Pages;

public partial class AuthPage : ContentPage
{
    public AuthPage(AuthViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}