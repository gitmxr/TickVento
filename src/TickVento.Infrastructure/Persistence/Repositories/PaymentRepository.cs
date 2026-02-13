
using TickVento.Application.Abstractions.Persistence;
using TickVento.Infrastructure.Persistence.Data;
using TickVento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TickVento.Infrastructure.Persistence.Repositories;

public class PaymentRepository(TickVentoDbContext dbContext) : IPaymentRepository
{
    private readonly TickVentoDbContext _dbContext = dbContext;
    
    public async Task AddAsync(Payment payment)
    {
        await _dbContext.AddAsync(payment);
    }

    public async Task<Payment?> GetByBookingIdAsync(Guid bookingId)
    {
        return await _dbContext.Payments
            .FirstOrDefaultAsync(p => p.BookingId == bookingId);
    }
    public Task UpdateAsync(Payment payment)
    {
        _dbContext.Payments.Update(payment);
        return Task.CompletedTask;
    }
    
}
