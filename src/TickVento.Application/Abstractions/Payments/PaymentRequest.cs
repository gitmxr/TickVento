
using TickVento.Domain.Enums;

namespace TickVento.Application.Abstractions.Payments;
public record PaymentRequest(
Guid BookingId,
decimal Amount,
PaymentMethod Method
);
