using CarPool.Domain.Common;
using CarPool.Domain.Users;

namespace CarPool.Domain.Trips;

public class Notification : Entity<NotificationId>
{
    public UserId ToUserId { get; private set; }
    public string NotificationText { get; private set; }
    public bool? IsRead { get; private set; }

    private Notification() { }

    private Notification(NotificationId notificationId, UserId toUserId, string notificationText)
        : base(notificationId)
    {
        ToUserId = toUserId;
        NotificationText = notificationText;
        IsRead = false;
    }

    public static Notification Create(UserId toUserId, string notificationText)
    {
        return new(
            NotificationId.CreateUnique(),
            toUserId,
            notificationText
        );
    }

    public void SetReadStatus(bool isRead)
    {
        IsRead = isRead;
    }
}

