
using TickVento.Application.Abstractions.Payments;
using TickVento.Domain.Enums;
namespace TickVento.Infrastructure.Payments;

public class SandboxPaymentGateway : IPaymentGateway
{
    public async Task<PaymentResult> ChargeAsync(PaymentRequest request)
    {
        // Simulate external API delay
        await Task.Delay(500);

        if (request.Amount <= 0)
            return Fail("Invalid amount.");
        
        if(request.Amount > 100_000)
            return Fail("Amount exceeds allowed limit.");

        if (!Enum.IsDefined(typeof(PaymentMethod), request.Method))
            return Fail("Invalid payment method.");

        if (request.Method == PaymentMethod.JazzCash && request.Amount > 50_000)
            return Fail("JazzCash cannot process amounts over 50,000.");
        
        if (request.Method == PaymentMethod.EasyPaisa && request.Amount > 50_000)
            return Fail("EasyPaisa cannot process amounts over 50,000.");

        var transactionId = Guid.NewGuid().ToString();
        return new PaymentResult(true, transactionId, null);

    }
    private PaymentResult Fail(string message)
    {
        return new PaymentResult
        (
            false,
            null,
            message
        );
    }
}
