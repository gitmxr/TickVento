
namespace TickVento.Application.Abstractions.Payments;
public record PaymentResult(
    bool IsSuccessful,
    string? TransactionId,
    string? FailureReason
);
