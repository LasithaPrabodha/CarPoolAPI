using System;
using AutoMapper;
using CarPool.Application.Trips.Commands;
using CarPool.Common;
using CarPool.WebAPI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.WebAPI.Controllers;

[Route("Trips")]
[Authorize]
public class TripController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public TripController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTripViewModel createTripViewModel)
    {
        Result result = await _mediator.Send(_mapper.Map<CreateTripCommand>(createTripViewModel));

        return result.Failed ? Problem(result.MessageWithErrors) : Ok();
    }

}