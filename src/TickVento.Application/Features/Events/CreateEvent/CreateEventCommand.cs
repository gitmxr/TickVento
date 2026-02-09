namespace TickVento.Application.Features.Events.CreateEvent;

public record CreateEventCommand(
    string Title,
    string Description,
    DateTime EventDate,
    string VenueName,
    int TotalSeats
);
