namespace TickVento.Application.Features.Users.RegisterUser;
public record RegisterUserResult(
    Guid UserId,
    string Email,
    string Message
);
