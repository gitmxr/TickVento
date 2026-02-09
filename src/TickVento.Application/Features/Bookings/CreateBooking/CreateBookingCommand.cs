

namespace TickVento.Application.Features.Bookings.CreateBooking;
public record CreateBookingCommand
(
    Guid UserId,
    Guid EventId,
    IEnumerable<string> SeatNumbers

);
