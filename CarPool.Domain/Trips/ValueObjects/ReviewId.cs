using System;
using CarPool.Domain.Common;

namespace CarPool.Domain.Trips;

public sealed class ReviewId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ReviewId(Guid value)
    {
        Value = value;
    }

    public static ReviewId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ReviewId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private ReviewId() { }
}

