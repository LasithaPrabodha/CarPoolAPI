using CarPool.Domain.Common;
using CarPool.Domain.Common.Models;
using CarPool.Domain.Users; 

namespace CarPool.Domain.Trips;

public class Trip : AggregateRoot<TripId, Guid>, IAggregateRoot
{
    public UserId DriverId { get; private set; }
    public Address Origin { get; private set; }
    public Address Destination { get; private set; }
    public DateTime DepartTime { get; private set; }
    public int AvailableSeats { get; private set; }
    public double PricePerSeat { get; private set; }
    public int? ViewedByCount { get; private set; }
    public Vehicle Vehicle { get; private set; }

    private List<Booking> _bookings = new();
    public IReadOnlyList<Booking> Bookings { get { return _bookings.AsReadOnly(); } private set { _bookings = value.ToList(); } }

    private List<Stop> _stops = new();
    public IReadOnlyList<Stop> Stops { get { return _stops.AsReadOnly(); } private set { _stops = value.ToList(); } }

    private Trip() { }

    private Trip(TripId tripId, UserId driverId, Address origin, Address destination, DateTime departTime,
        int availableSeats, double pricePerSeat, Vehicle vehicle)
        :base (tripId)
    {
        DriverId = driverId;
        Origin = origin;
        Destination = destination;
        DepartTime = departTime;
        AvailableSeats = availableSeats;
        PricePerSeat = pricePerSeat;
        ViewedByCount = 0;
        Vehicle = vehicle;
    }

    public static Trip Create(UserId driverId, Address origin, Address destination, DateTime departTime,
        int availableSeats, double pricePerSeat, Vehicle vehicle)
    {
        return new(
            TripId.CreateUnique(),
            driverId,
            origin,
            destination,
            departTime,
            availableSeats,
            pricePerSeat,
            vehicle
        );

    }



    public void IncreaseViewCount(int count = 1)
    {
        ViewedByCount += count;
    }

    public void AddBooking(UserId userId, int requiredSeats, string? description)
    {

        if (AvailableSeats == 0)
            throw new AddBookingToTripException("No seats available");

        var booking = Booking.Create(userId, requiredSeats, description);

        AvailableSeats -= requiredSeats;

        _bookings.Add(booking);
    }

    public void AddStop(Address address)
    {
        var stop = Stop.Create(address);
        _stops.Add(stop);
    }
}

