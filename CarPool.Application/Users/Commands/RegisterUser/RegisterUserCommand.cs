using CarPool.Application.Contracts;
using CarPool.Application.DTOs;
using CarPool.Common;
using CarPool.Domain.Users;
using MapsterMapper;
using MediatR;

namespace CarPool.Application.Users.Commands;

public record RegisterUserCommand(string Username, string Name, string Email, string Password, List<string> Roles) : IRequest<Result>
{
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IApplicationUserService _applicationUserService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IApplicationUserService applicationUserService, IUserRepository userRepository, IMapper mapper)
    {
        _applicationUserService = applicationUserService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = new Result();
        var userCreateResult = await _applicationUserService.CreateUser(_mapper.Map<UserDTO>(request), request.Password, request.Roles, true);

        if (userCreateResult.Failed)
            return result.Failed().WithMessage(userCreateResult.MessageWithErrors);
        

        var user = User.Create(request.Username, request.Name, request.Email);

        _userRepository.Add(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return result.Successful();

    }
}

