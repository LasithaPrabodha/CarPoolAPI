using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public sealed class BookingId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private BookingId(Guid value)
    {
        Value = value;
    }

    public static BookingId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static BookingId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private BookingId() { }
}

