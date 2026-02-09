using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Entities;

namespace TickVento.Application.Abstractions.Persistence
{
    public interface IPaymentRepository
    {
        public Task AddAsync(Payment payment);
        public Task<Payment?> GetByBookingIdAsync(Guid bookingId);
        public Task UpdateAsync(Payment payment);

    }
}
