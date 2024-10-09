using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBookingManager.Core.Interfaces;
using GymBookingManager.Core.Models;
using GymBookingManager.Core.Services;
using GymBookingManager.Maui.Client.Services;

namespace GymBookingManager.Maui.Client.ViewModels;

public partial class AuthViewModel : ObservableObject
{
    private readonly AppState _appState;
    private readonly IDialogService _dialogService;
    private readonly List<User> _registeredUsers = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string _username = string.Empty;

    private bool _isAuthenticated;

    public bool IsAuthenticated
    {
        get => _isAuthenticated;
        private set
        {
            if (_isAuthenticated == value)
                return;

            _isAuthenticated = value;
            OnPropertyChanged();
            AuthStateChanged?.Invoke(_isAuthenticated);
        }
    }

    public event Action<bool>? AuthStateChanged;

    public AuthViewModel(
        AppState appState,
        IDialogService dialogService)
    {
        _appState = appState;
        _dialogService = dialogService;
    }

    private bool CanAuthenticate() => !string.IsNullOrWhiteSpace(Username);

    [RelayCommand(CanExecute = nameof(CanAuthenticate))]
    private async Task LogIn()
    {
        if (!HasUser(Username))
        {
            await _dialogService.ShowAlertAsync("Error", "Username not found, register first.", "OK");
            return;
        }

        Authenticate();
    }

    [RelayCommand(CanExecute = nameof(CanAuthenticate))]
    private async Task Register()
    {
        if (HasUser(Username))
        {
            await _dialogService.ShowAlertAsync("Error", "Username already taken, try another.", "OK");
            return;
        }

        _registeredUsers.Add(new User { Username = Username });
        Authenticate();
    }

    public void LogOut()
    {
        MainThread.BeginInvokeOnMainThread(() => IsAuthenticated = false);
    }

    private void Authenticate()
    {
        _appState.CurrentUser = new User { Username = Username };

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Username = string.Empty;
            IsAuthenticated = true;
        });
    }

    private bool HasUser(string username) =>
        _registeredUsers.Exists(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
}