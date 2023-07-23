using System;
using CarPool.Application.Contracts;
using CarPool.Application.DTOs;
using CarPool.Common;
using CarPool.WebAPI.ViewModels;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.WebAPI.Controllers;

[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserAuthenticationService _userAuthenticationService;


    public AuthController(IMapper mapper, IUserAuthenticationService userAuthenticationService)
    {
        _mapper = mapper;
        _userAuthenticationService = userAuthenticationService;
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginModel)
    {
        Result<AuthResponseDTO> result = await _userAuthenticationService.Login(_mapper.Map<LoginRequestDTO>(loginModel));

        return result.Succeeded
            ? Ok(_mapper.Map<AuthResponseViewModel>(result.Data))
            : (IActionResult)Problem(statusCode: StatusCodes.Status400BadRequest, title: result.MessageWithErrors);
    }
}

