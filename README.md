# Report

### Design

- **Auth Page**: Responsible for creating a `User` by registering a new user or logging into an existing account.
- **WorkoutListPage**: Displays an overview and status of available workouts, including their start dates. users can
  book or cancel workouts here.

### Scalability & Event-driven programming

I used the MVVM pattern to enhance scalability. The `Model` resides in the Core class library, making it independent of
the user interface. The `User` and `Workout` classes represent the core business entities. The BookingService class
manages the booking and cancellation of workout sessions, handling user interactions while logged in. It stores the
booking data internally, with most of the core logic encapsulated here for reuse across different components.

The `View` is designed for simplicity but structured for future extensibility. I extracted the CollectionView control
and its DataTemplate for displaying each workout into separate controls to reduce complexity, making it easier to manage
and modify.

The `ViewModels` could be moved to a separate class library if not for the usage of thread switching via `MainThread`
from `Microsoft.Maui.Essentials`. However, I am using `ICommand` implementations for event-driven programming,
abstracted by the `CommunityToolkit.Mvvm` package. This makes the viewmodels reusable, even if I switch the GUI
framework from MAUI to WPF. Additionally, a `WorkoutViewModel` was created to encapsulate the details of the Workout
class. This viewmodel exposes properties like `IsBooked` and `ReservedSeats` as observable, focusing on aspects that
the UI is primarily interested in.

### Notes

While persisting the user state wasn't strictly necessary, I decided to experiment with an in-memory implementation out
of curiosity. This added an interesting layer, allowing me to see how the system's behavior changes when switching
between users and observing the persistent state across sessions.
