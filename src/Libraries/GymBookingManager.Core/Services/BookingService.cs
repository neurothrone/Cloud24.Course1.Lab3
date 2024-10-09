using GymBookingManager.Core.Enums;
using GymBookingManager.Core.Models;
using GymBookingManager.Core.Utils;

namespace GymBookingManager.Core.Services;

public class BookingService
{
    # region Workouts

    // Descriptions were taken from the Swedish gym STC´s official workouts and translated
    // to English with Google Translate.

    private readonly List<Workout> _workouts =
    [
        new()
        {
            Id = 1,
            Title = "STC Functional",
            Description = "STC Functional is a varied and medium to high-intensity workout that increases your " +
                          "resilience, strength, speed, agility and explosiveness. Try it on a fun and interval-based " +
                          "workout that always offers sweat and panting.",
            WorkoutCategory = WorkoutCategory.Functional,
            StartTime = DateTimeExtensions.TomorrowAt(hour: 17, minute: 30),
            Duration = TimeSpan.FromMinutes(30),
            MaxSeats = 20,
            ReservedSeats = 20
        },
        new()
        {
            Id = 2,
            Title = "Yin Yoga",
            Description = "Yoga strengthens the body and helps you relax mentally. Classic yoga positions and " +
                          "breathing helps you stretch and find balance in a way that unites both body and mind!",
            WorkoutCategory = WorkoutCategory.Yoga,
            StartTime = DateTimeExtensions.TomorrowAt(hour: 18, minute: 15),
            Duration = TimeSpan.FromMinutes(60),
            MaxSeats = 15,
            ReservedSeats = 14
        },
        new()
        {
            Id = 3,
            Title = "STC Cycling",
            Description = "Cycle away stress, bad thoughts and sore joints. Spinning is a gentle form of exercise " +
                          "that fits most people! And don't worry, we'll guide you through the entire session so you " +
                          "know when to put on resistance, recover or give it your all.",
            WorkoutCategory = WorkoutCategory.Cycling,
            StartTime = DateTimeExtensions.TodayAt(hour: 19, minute: 0).AddDays(2),
            Duration = TimeSpan.FromMinutes(30),
            MaxSeats = 25,
            ReservedSeats = Random.Shared.Next(4, 11)
        },
        new()
        {
            Id = 4,
            Title = "STC Core",
            Description = "STC Core is a short, effective workout that strengthens your body from the inside out. " +
                          "The focus is on strengthening the trunk muscles and the exercises are largely done with " +
                          "your own body weight, but also tools such as rubber bands.",
            WorkoutCategory = WorkoutCategory.Core,
            StartTime = DateTimeExtensions.TodayAt(hour: 17, minute: 30).AddDays(3),
            Duration = TimeSpan.FromMinutes(30),
            MaxSeats = 30,
            ReservedSeats = Random.Shared.Next(1, 4)
        },
        new()
        {
            Id = 5,
            Title = "STC Running",
            Description = "Jane Doe welcomes you to STC´s running training.",
            WorkoutCategory = WorkoutCategory.Running,
            StartTime = DateTimeExtensions.TodayAt(hour: 18, minute: 15).AddDays(3),
            Duration = TimeSpan.FromMinutes(60),
            MaxSeats = 15,
            ReservedSeats = 0
        }
    ];

    #endregion

    private readonly Dictionary<string, List<int>> _userBookings = new();

    public List<Workout> GetWorkouts() => _workouts;

    public List<int> GetUserBookings(string username) => _userBookings.TryGetValue(username, out List<int>? value)
        ? value
        : [];

    public bool BookWorkout(string username, Workout workout)
    {
        InitializeIfNewUser(username);

        if (_userBookings[username].Contains(workout.Id) || workout.IsFullyBooked)
            return false;

        var foundWorkout = _workouts.FirstOrDefault(w => w.Id == workout.Id);
        if (foundWorkout is not null)
            foundWorkout.ReservedSeats += 1;

        _userBookings[username].Add(workout.Id);
        return true;
    }

    public bool CancelWorkout(string username, Workout workout)
    {
        InitializeIfNewUser(username);

        if (!_userBookings[username].Contains(workout.Id))
            return false;

        var foundWorkout = _workouts.FirstOrDefault(w => w.Id == workout.Id);
        if (foundWorkout is not null)
            foundWorkout.ReservedSeats -= 1;

        _userBookings[username].Remove(workout.Id);
        return true;
    }

    private void InitializeIfNewUser(string username)
    {
        if (!_userBookings.ContainsKey(username))
            _userBookings.Add(username, []);
    }
}