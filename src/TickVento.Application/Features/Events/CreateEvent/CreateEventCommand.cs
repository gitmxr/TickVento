using TickVento.Domain.Enums;

namespace TickVento.Application.Features.Events.CreateEvent;

public record CreateEventCommand(
    string Title,
    string Description,
    DateTime EventDate,
    string VenueName,
    Dictionary<SeatCategory, decimal> SeatPrices,        // Price per category
    Dictionary<SeatCategory, int> SeatCategoryCounts     // Number of seats per category
);
