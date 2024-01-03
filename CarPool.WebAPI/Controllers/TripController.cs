using System;
using CarPool.Application.DTOs;
using CarPool.Application.Trips.Commands;
using CarPool.Application.Trips.Queries;
using CarPool.Common;
using CarPool.Domain.Trips;
using CarPool.WebAPI.ViewModels;
using MapsterMapper;
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
    public async Task<IActionResult> Create(CreateTripViewModel viewModel)
    {
        Result result = await _mediator.Send(_mapper.Map<CreateTripCommand>(viewModel));

        return result.Failed ? Problem(result.MessageWithErrors) : Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] TripsRequestViewModel viewModel)
    {
        Result<PagedResult<TripResponseDTO>> result = await _mediator.Send(new GetAllTripsQuery(viewModel.Page, viewModel.PageSize));

        return result.Failed ? Problem(result.Exception.Message) : Ok(_mapper.Map<PagedResult<TripResponseViewModel>>(result.Data));
    }

    [Route("{id:guid}")]
    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        Result<TripResponseDTO> result = await _mediator.Send(new GetTripQuery(TripId.Create(id)));

        return result.Failed ? Problem(result.Exception.Message) : Ok(_mapper.Map<TripResponseViewModel>(result.Data));
    }


}