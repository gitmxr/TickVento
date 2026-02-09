using TickVento.Application.Abstractions.Persistence;
using TickVento.Application.Common.Exceptions;
using TickVento.Domain.Entities;

namespace TickVento.Application.Features.Users.RegisterUser;

public class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand command)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email);

        if (existingUser is not null)
            throw new BusinessRuleException("Email is already registered.");

        var user = new User(
            command.Email,
            command.FullName,
            command.Role);

        await _userRepository.AddAsync(user);

        return new RegisterUserResult(
            user.Id,
            user.Email,
            "User successfully registered"
        );
    }
}
