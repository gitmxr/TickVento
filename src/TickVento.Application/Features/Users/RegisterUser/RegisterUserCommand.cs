using TickVento.Domain.Enums;

namespace TickVento.Application.Features.Users.RegisterUser;

public record RegisterUserCommand(
    string Email,
    string FullName,
    UserRole Role
);
