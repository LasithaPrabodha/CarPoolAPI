using CarPool.Application.DTOs;
using CarPool.Common;
using CarPool.Domain.Users;

namespace CarPool.Application.Contracts;

public interface IApplicationUserService
{
    /// <summary>
    /// Creates a new user according to the provided <paramref name="user"/>
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="roles"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    Task<Result> CreateUser(UserDTO user, string password, List<string> roles, bool isActive);

    /// <summary>
    /// Activates the application user with username <paramref name="username"/>
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<Result> ActivateUser(string username);

    /// <summary>
    /// Deactivates the application user with username <paramref name="username"/>
    /// </summary>
    /// <returns></returns>
    Task<Result> DeactivateUser(string username);

    /// <summary>
    /// Adds a role to a user
    /// </summary>
    /// <param name="request">the role assignment request</param>
    /// <returns></returns>
    Task<Result> AddRoles(RoleAssignmentRequestDTO request);

    /// <summary>
    /// Removes a role from a user
    /// </summary>
    /// <param name="request">the role assignment request</param>
    /// <returns></returns>
    Task<Result> RemoveRoles(RoleAssignmentRequestDTO request);

    /// <summary>
    /// Check if user is has a role
    /// </summary>
    /// <param name="userId">the user Id</param>
    /// <param name="role"></param>
    /// <returns>whether user has the role</returns>
    Task<Result> IsInRoleAsync(string userId, string role);

    Task<Result> AuthorizeAsync(string userId, string policyName);

    /// <summary>
    /// Get user
    /// </summary>
    /// <param name="id">the user ID</param>
    /// <returns> a <see cref="User"/></returns>
    Task<Result<UserDTO?>> GetUser(string id);

    /// <summary>
    /// Changes a user's password
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<Result> ChangePassword(string username, string password);

    Task<Result> UpdateUserDetails(UpdateUserDetailsDTO request);
}

