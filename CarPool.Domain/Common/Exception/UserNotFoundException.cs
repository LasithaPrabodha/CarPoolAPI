using System;

namespace CarPool.Domain.Common;

public class UserNotFoundException:Exception
{
    public UserNotFoundException(string userId) : base($"User \"{userId}\" not found.")
    {
    }
}

