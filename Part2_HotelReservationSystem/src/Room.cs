namespace Part2_HotelReservationSystem;

public class Room
{
    public int RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (roomNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(roomNumber), "Room number must be positive.");

        if (nightlyRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(nightlyRate), "Nightly rate must be positive.");

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
    }

    public void ChangeNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(newRate), "Nightly rate must be positive.");

        NightlyRate = newRate;
    }

    public void StartMaintenance() => IsUnderMaintenance = true;
    public void EndMaintenance() => IsUnderMaintenance = false;
}
