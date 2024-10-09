using CommunityToolkit.Maui;
using GymBookingManager.Core.Interfaces;
using GymBookingManager.Core.Services;
using GymBookingManager.Maui.Client.Services;
using GymBookingManager.Maui.Client.ViewModels;
using GymBookingManager.Maui.Client.Views.Pages;
using Microsoft.Extensions.Logging;

namespace GymBookingManager.Maui.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Source:
            // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/alerts/toast#platform-specific-initialization
            .UseMauiCommunityToolkit(options => { options.SetShouldEnableSnackbarOnWindows(true); })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // !: Register Services
        builder.Services.AddSingleton<AppState>();
        builder.Services.AddSingleton<BookingService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();

        // !: Register ViewModels
        builder.Services.AddSingleton<AuthViewModel>();
        builder.Services.AddSingleton<WorkoutListViewModel>();

        // !: Register Pages
        builder.Services.AddSingleton<WorkoutListPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}