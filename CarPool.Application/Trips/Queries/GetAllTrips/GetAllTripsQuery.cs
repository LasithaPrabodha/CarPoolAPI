using System;
using CarPool.Application.Contracts;
using CarPool.Application.DTOs;
using CarPool.Common;
using CarPool.Domain.Trips;
using MapsterMapper;
using MediatR;

namespace CarPool.Application.Trips.Queries;

public record GetAllTripsQuery(int Page, int PageSize) : IRequest<Result<PagedResult<TripResponseDTO>>>
{

}

public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQuery, Result<PagedResult<TripResponseDTO>>>
{

    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public GetAllTripsQueryHandler(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }


    public Task<Result<PagedResult<TripResponseDTO>>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<PagedResult<TripResponseDTO>>();
        var trips = _tripRepository.GetAll().GetPaged(request.Page, request.PageSize);

        result.Data = _mapper.Map<PagedResult<TripResponseDTO>>(trips);

        return Task.FromResult(result);

    }
}
