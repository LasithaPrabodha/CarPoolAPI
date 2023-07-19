using System; 
using CarPool.Application.DTOs;
using CarPool.Common;

namespace CarPool.Application.Contracts;

public interface IUserAuthenticationService
{
    Task<Result<AuthResponseDTO>> Login(LoginRequestDTO loginRequest);
    Task<Result> Logout();
}

