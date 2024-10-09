namespace GymBookingManager.Core.Interfaces;

public interface IDialogService
{
    Task ShowAlertAsync(string title, string message, string accept);
    Task<bool> ShowPromptAsync(string title, string message, string accept, string cancel);
    Task ShowToastAsync(string message);
    Task ShowSnackbarAsync(string message, bool isDestructive = false);
}