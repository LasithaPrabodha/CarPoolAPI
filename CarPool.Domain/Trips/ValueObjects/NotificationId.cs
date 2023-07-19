using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public sealed class NotificationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private NotificationId(Guid value)
    {
        Value = value;
    }

    public static NotificationId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static NotificationId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private NotificationId() { }
}

