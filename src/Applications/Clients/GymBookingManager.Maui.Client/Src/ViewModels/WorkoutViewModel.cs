using CommunityToolkit.Mvvm.ComponentModel;
using GymBookingManager.Core.Enums;

namespace GymBookingManager.Maui.Client.ViewModels;

public partial class WorkoutViewModel : ObservableObject
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public WorkoutCategory WorkoutCategory { get; set; }

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public int MaxSeats { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFullyBooked))]
    [NotifyPropertyChangedFor(nameof(SeatsStatus))]
    private int _reservedSeats;

    [ObservableProperty]
    private bool _isBooked;

    public bool IsFullyBooked => ReservedSeats >= MaxSeats;

    public string SeatsStatus => $"{ReservedSeats} / {MaxSeats}";
}