using Part2_HotelReservationSystem;

var hotel = new Hotel();

var guest = new Guest(1, "Ahmed Ali", "01000000000");
var secondGuest = new Guest(2, "Mona Hassan", "01111111111");
hotel.RegisterGuest(guest);
hotel.RegisterGuest(secondGuest);

var room101 = new Room(101, RoomType.Double, 1500m);
var room201 = new Room(201, RoomType.Suite, 3000m);
hotel.AddRoom(room101);
hotel.AddRoom(room201);

Console.WriteLine("=== Hotel Reservation System ===");

Reservation reservation = hotel.CreateReservation(
    1001,
    guest,
    room101,
    new DateOnly(2026, 9, 20),
    new DateOnly(2026, 9, 23));

Console.WriteLine($"Created reservation #{reservation.ReservationId}");
Console.WriteLine($"Guest: {reservation.Guest.FullName}");
Console.WriteLine($"Room: {reservation.Room.RoomNumber} ({reservation.Room.RoomType})");
Console.WriteLine($"Status: {reservation.Status}");
Console.WriteLine($"Nights: {reservation.Nights}");
Console.WriteLine($"Total cost: {reservation.TotalCost:F2}");

reservation.Confirm();
reservation.CheckIn();
Console.WriteLine($"Status after check-in: {reservation.Status}");

reservation.CheckOut();
Console.WriteLine($"Status after check-out: {reservation.Status}");

Console.WriteLine($"Reservation history for {guest.FullName}: {guest.Reservations.Count} item(s)");

Console.WriteLine("\n=== Validation demonstrations ===");

Try("Invalid dates", () =>
    hotel.CreateReservation(
        1002,
        secondGuest,
        room201,
        new DateOnly(2026, 9, 25),
        new DateOnly(2026, 9, 25)));

room201.StartMaintenance();
Try("Booking a room under maintenance", () =>
    hotel.CreateReservation(
        1003,
        secondGuest,
        room201,
        new DateOnly(2026, 9, 26),
        new DateOnly(2026, 9, 28)));
room201.EndMaintenance();

Try("Negative pricing", () => room201.ChangeNightlyRate(0m));

Reservation confirmed = hotel.CreateReservation(
    1004,
    secondGuest,
    room201,
    new DateOnly(2026, 10, 1),
    new DateOnly(2026, 10, 5));

Try("Check-in before confirmation", confirmed.CheckIn);
confirmed.Confirm();
confirmed.Cancel();
Try("Cancel after already cancelled", confirmed.Cancel);

Reservation baseReservation = hotel.CreateReservation(
    1005,
    guest,
    room201,
    new DateOnly(2026, 11, 1),
    new DateOnly(2026, 11, 5));

Try("Double booking", () =>
    hotel.CreateReservation(
        1006,
        secondGuest,
        room201,
        new DateOnly(2026, 11, 3),
        new DateOnly(2026, 11, 7)));

Console.WriteLine("\nAll demonstrations completed.");

static void Try(string name, Action action)
{
    try
    {
        action();
        Console.WriteLine($"{name}: unexpectedly accepted.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{name}: rejected -> {ex.Message}");
    }
}
