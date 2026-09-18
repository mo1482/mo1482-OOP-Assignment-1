namespace Part2_HotelReservationSystem;

public class Hotel
{
    private readonly List<Guest> _guests = new();
    private readonly List<Room> _rooms = new();
    private readonly List<Reservation> _reservations = new();

    public IReadOnlyList<Guest> Guests => _guests.AsReadOnly();
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public void RegisterGuest(Guest guest)
    {
        ArgumentNullException.ThrowIfNull(guest);

        if (_guests.Any(g => g.GuestId == guest.GuestId))
            throw new InvalidOperationException($"Guest id {guest.GuestId} already exists.");

        _guests.Add(guest);
    }

    public void AddRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        if (_rooms.Any(r => r.RoomNumber == room.RoomNumber))
            throw new InvalidOperationException($"Room number {room.RoomNumber} already exists.");

        _rooms.Add(room);
    }

    public Reservation CreateReservation(
        int reservationId,
        Guest guest,
        Room room,
        DateOnly checkInDate,
        DateOnly checkOutDate)
    {
        ArgumentNullException.ThrowIfNull(guest);
        ArgumentNullException.ThrowIfNull(room);

        if (_reservations.Any(r => r.ReservationId == reservationId))
            throw new InvalidOperationException($"Reservation id {reservationId} already exists.");

        if (room.IsUnderMaintenance)
            throw new InvalidOperationException($"Room {room.RoomNumber} is under maintenance and cannot be booked.");

        bool overlaps = _reservations.Any(existing =>
            existing.Room == room &&
            existing.Status is not (ReservationStatus.Cancelled or ReservationStatus.CheckedOut) &&
            checkInDate < existing.CheckOutDate &&
            checkOutDate > existing.CheckInDate);

        if (overlaps)
            throw new InvalidOperationException($"Room {room.RoomNumber} already has an overlapping active reservation.");

        var reservation = new Reservation(
            reservationId,
            guest,
            room,
            checkInDate,
            checkOutDate);

        _reservations.Add(reservation);
        guest.AddReservation(reservation);
        return reservation;
    }
}
