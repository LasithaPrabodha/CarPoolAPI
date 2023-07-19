using CarPool.Application.Contracts;
using CarPool.Common;
using CarPool.Domain.Users;
using MediatR;

namespace CarPool.Application.Users.Commands;

public record RegisterUserCommand(string Username, string Name, string Email, string Password, List<string> Roles) : IRequest<Result>
{
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
{
    private readonly IApplicationUserService _applicationUserService;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IApplicationUserService applicationUserService, IUserRepository userRepository)
    {
        _applicationUserService = applicationUserService;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = new Result();
        var user = User.Create(request.Username, request.Name, request.Email);

        var userCreateResult = await _applicationUserService.CreateUser(user, request.Password, request.Roles, true);

        if (userCreateResult.Failed)
        {
            return result.Failed().WithMessage(userCreateResult.MessageWithErrors);
        }

        _userRepository.Add(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return result.Successful();

    }
}

