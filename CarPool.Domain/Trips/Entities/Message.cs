using CarPool.Domain.Common;
using CarPool.Domain.Users;

namespace CarPool.Domain.Trips;

public class Message : Entity<MessageId>
{
    public UserId SenderId { get; private set; }
    public UserId ReceiverId { get; private set; }
    public string MessageText { get; private set; }

    private Message() { }

    private Message(MessageId messageId, UserId senderId, UserId receiverId, string messageText)
        : base(messageId)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        MessageText = messageText;
    }

    public static Message Create(UserId senderId, UserId receiverId, string messageText)
    {
        return new(
            MessageId.CreateUnique(),
            senderId,
            receiverId,
            messageText
        );
    }

}

