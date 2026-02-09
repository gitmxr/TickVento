
using TickVento.Domain.Enums;

namespace TickVento.Application.Features.Events.CreateEvent;
public record CreateEventResult(
    Guid EventId,
    string Title,
    Dictionary<SeatCategory, int> SeatsPerCategory
);

