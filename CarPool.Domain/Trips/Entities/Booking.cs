using CarPool.Domain.Common;
using CarPool.Domain.Users;

namespace CarPool.Domain.Trips;

public sealed class Booking : Entity<BookingId>
{
    public int RequiredSeats { get; private set; }
    public string? Description { get; private set; }
    public bool? Accepted { get; private set; }
    public Review? Review { get; private set; }
    public UserId UserId { get; private set; }

    private List<Message> _messages = new();
    public IReadOnlyList<Message> Messages { get { return _messages.AsReadOnly(); } private set { _messages = value.ToList(); } }

    private List<Notification> _notifications = new();
    public IReadOnlyList<Notification> Notifications { get { return _notifications.AsReadOnly(); } private set { _notifications = value.ToList(); } }


    private Booking() { }

    private Booking(BookingId bookingId, UserId userId, int requiredSeats, string? description)
        : base(bookingId)
    {
        UserId = userId;
        RequiredSeats = requiredSeats;
        Description = description;
        Accepted = false;
        Review = null;
    }

    public static Booking Create(UserId userId, int requiredSeats, string? description)
    {
        return new(BookingId.CreateUnique(), userId, requiredSeats, description);
    }

    public void AcceptBooking()
    {
        Accepted = true;
    }

    public void DeclineBooking()
    {
        Accepted = false;
    }

    public void AddReview(UserId userId, int rating, string reviewText)
    {
        Review = Review.Create(userId, rating, reviewText);
    }

    public void CreateMessage(UserId senderId, UserId receiverId, string messageText)
    {
        var message = Message.Create(senderId, receiverId, messageText);

        _messages.Add(message);
    }

    public void CreateNotification(UserId userId, string notificationText)
    {
        var notification = Notification.Create(userId, notificationText);

        _notifications.Add(notification);
    }

}

