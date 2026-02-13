
namespace TickVento.Application.Abstractions.Payments
{
    public interface IPaymentGateway
    {
        Task<PaymentResult> ChargeAsync(PaymentRequest request);
    }
}
