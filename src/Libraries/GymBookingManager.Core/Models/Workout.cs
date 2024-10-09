using GymBookingManager.Core.Enums;

namespace GymBookingManager.Core.Models;

public class Workout
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkoutCategory WorkoutCategory { get; set; }

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public int MaxSeats { get; set; }
    public int ReservedSeats { get; set; }

    public bool IsFullyBooked => ReservedSeats >= MaxSeats;
}