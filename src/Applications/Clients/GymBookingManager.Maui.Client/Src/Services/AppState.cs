using GymBookingManager.Core.Models;

namespace GymBookingManager.Maui.Client.Services;

public class AppState
{
    public User CurrentUser { get; set; } = new();

    public string Username => CurrentUser.Username;
}