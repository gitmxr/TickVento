using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickVento.Application.Abstractions.Payments
{
    public interface IPaymentGateway
    {
        Task<PaymentResult> ChargeAsync(PaymentRequest request);
    }
}
