using System;
using CarPool.Application.Contracts;
using CarPool.Application.DTOs;
using CarPool.Common;
using CarPool.Domain.Common;
using CarPool.Domain.Trips;
using MapsterMapper;
using MediatR;

namespace CarPool.Application.Trips.Queries;

public record GetTripQuery(Guid TripId) : IRequest<Result<TripResponseDTO>>
{
}

public class GetTripQueryHandler : IRequestHandler<GetTripQuery, Result<TripResponseDTO>>
{

    private readonly ITripRepository _tripRepository;
    private readonly IMapper _mapper;

    public GetTripQueryHandler(ITripRepository tripRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public async Task<Result<TripResponseDTO>> Handle(GetTripQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<TripResponseDTO>();
        var trip = await _tripRepository.GetSingleAsync(t => t.Id == TripId.Create(request.TripId));

        if (trip is null)
            return result.Failed().WithException(new TripNotFoundException(request.TripId));

        var tripDTO = _mapper.Map<TripResponseDTO>(trip);
        result.Data = tripDTO;

        return result.Successful();

    }
}
