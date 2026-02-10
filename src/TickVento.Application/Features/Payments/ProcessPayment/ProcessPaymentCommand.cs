using TickVento.Domain.Enums;
namespace TickVento.Application.Features.Payments.ProcessPayment;
public record ProcessPaymentCommand(
   Guid BookingId,
    //decimal Amount,
    PaymentMethod Method
);


