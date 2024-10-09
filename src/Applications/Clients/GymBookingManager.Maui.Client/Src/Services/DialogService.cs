using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GymBookingManager.Core.Interfaces;

namespace GymBookingManager.Maui.Client.Services;

public class DialogService : IDialogService
{
    public async Task ShowAlertAsync(
        string title,
        string message,
        string accept) => await MainThread.InvokeOnMainThreadAsync(
        () => Application.Current?.MainPage?.DisplayAlert(title, message, accept));

    public async Task<bool> ShowPromptAsync(
        string title,
        string message,
        string accept,
        string cancel) => await MainThread.InvokeOnMainThreadAsync(
        () => Application.Current?.MainPage?.DisplayAlert(title, message, accept, cancel));

    public async Task ShowToastAsync(string message)
    {
        // Source:
        // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/alerts/toast#c
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(message, duration: ToastDuration.Short, textSize: 14);
        await toast.Show(cancellationTokenSource.Token);
    }

    public async Task ShowSnackbarAsync(string message, bool isDestructive = false)
    {
        // Source:
        // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/alerts/snackbar#c
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = isDestructive ? Colors.DarkRed : Colors.SeaGreen,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.LightGray,
            CornerRadius = new CornerRadius(10),
            Font = Microsoft.Maui.Font.SystemFontOfSize(16),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(16),
        };

        var snackbar = Snackbar.Make(
            message: message,
            duration: TimeSpan.FromSeconds(2),
            visualOptions: snackbarOptions);

        await snackbar.Show(cancellationTokenSource.Token);
    }
}