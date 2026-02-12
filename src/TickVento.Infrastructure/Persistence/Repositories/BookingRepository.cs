using TickVento.Infrastructure.Persistence.Data;
using TickVento.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TickVento.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TickVento.Infrastructure.Persistence.Repositories;

public class BookingRepository(TickVentoDbContext dbContext) : IBookingRepository
{
    private readonly TickVentoDbContext _dbContext = dbContext;
    public async Task<Booking?> GetByIdAsync(Guid bookingId)
    {
        return await _dbContext.Bookings
            .Include(b => b.Seats)
            .Include(b => b.Event)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
    }

    public async Task AddAsync(Booking booking)
    {
        await _dbContext.AddAsync(booking);
    }

    public Task UpdateAsync(Booking booking)
    {
        _dbContext.Update(booking);
        return Task.CompletedTask;
    }

}


    

    
