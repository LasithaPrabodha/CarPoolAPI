using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public sealed class MessageId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MessageId(Guid value)
    {
        Value = value;
    }

    public static MessageId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static MessageId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private MessageId() { }
}

