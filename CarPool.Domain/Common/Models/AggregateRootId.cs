using System;
using System.IO;

namespace CarPool.Domain.Common;

public abstract class AggregateRootId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}

