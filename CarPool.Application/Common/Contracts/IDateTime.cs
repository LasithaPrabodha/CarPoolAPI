using System;
namespace CarPool.Application.Contracts;

public interface IDateTime
{
    DateTime Now { get; }
}

