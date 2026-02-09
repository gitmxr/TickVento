

namespace TickVento.Application.Features.Bookings.CreateBooking
{
    public record CreateBookingResult(
    Guid BookingId,
    string EventTitle,
    IEnumerable<string> SeatNumbers
    );

}
