
using CarPool.Application.Users.Commands;
using CarPool.Common;
using CarPool.WebAPI.ViewModels;
using MapsterMapper;
using MediatR;
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
    public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
    {
        Result result = await _mediator.Send(_mapper.Map<RegisterUserCommand>(viewModel));

        return result.Failed ? Problem(result.MessageWithErrors) : Ok();
    }
}

