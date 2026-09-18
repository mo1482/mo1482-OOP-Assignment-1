namespace Part2_HotelReservationSystem;

public class Reservation
{
    public int ReservationId { get; }
    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }
    public Room Room { get; }
    public Guest Guest { get; }
    public ReservationStatus Status { get; private set; }

    public int Nights => CheckOutDate.DayNumber - CheckInDate.DayNumber;
    public decimal TotalCost => Nights * Room.NightlyRate;

    internal Reservation(
        int reservationId,
        Guest guest,
        Room room,
        DateOnly checkInDate,
        DateOnly checkOutDate)
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Check-out date must be strictly after check-in date.");

        ArgumentNullException.ThrowIfNull(guest);
        ArgumentNullException.ThrowIfNull(room);

        ReservationId = reservationId;
        Guest = guest;
        Room = room;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = ReservationStatus.Pending;
    }

    public void Confirm()
    {
        EnsureStatus(ReservationStatus.Pending, "Only a Pending reservation can be confirmed.");
        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        EnsureStatus(ReservationStatus.Confirmed, "A reservation must be Confirmed before check-in.");
        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        EnsureStatus(ReservationStatus.CheckedIn, "Only a CheckedIn reservation can be checked out.");
        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status is not (ReservationStatus.Pending or ReservationStatus.Confirmed))
            throw new InvalidOperationException("Only Pending or Confirmed reservations can be cancelled.");

        Status = ReservationStatus.Cancelled;
    }

    private void EnsureStatus(ReservationStatus expected, string message)
    {
        if (Status != expected)
            throw new InvalidOperationException(message);
    }
}
