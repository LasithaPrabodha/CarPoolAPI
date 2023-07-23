using CarPool.Application.Contracts;

namespace CarPool.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

