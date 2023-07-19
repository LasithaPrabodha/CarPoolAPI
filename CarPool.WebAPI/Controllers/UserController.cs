using System;
using AutoMapper;
using CarPool.Application.Contracts;
using CarPool.Application.Users.Commands;
using CarPool.Common;
using CarPool.WebAPI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.WebAPI.Controllers;

[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public UserController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserViewModel createUserModel)
    {
        Result result = await _mediator.Send(_mapper.Map<RegisterUserCommand>(createUserModel));

        return result.Failed ? Problem(result.MessageWithErrors) : Ok();
    }
}

