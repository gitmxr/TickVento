using Microsoft.EntityFrameworkCore;
using TickVento.Application.Abstractions.Persistence;
using TickVento.Domain.Entities;
using TickVento.Infrastructure.Persistence.Data;

namespace TickVento.Infrastructure.Persistence.Repositories;

public class EventRepository(TickVentoDbContext dbContext) : IEventRepository
{
    private readonly TickVentoDbContext _dbContext = dbContext;

    public async Task<Event?> GetByIdAsync(Guid eventId)
    {
        return await _dbContext.Events.FindAsync(eventId);
    }
    public async Task<Event?> GetByIdWithSeatsAsync(Guid eventId)
    {
        return await _dbContext.Events
            .Include(e => e.Seats)
            .FirstOrDefaultAsync(e => e.Id == eventId);
    }
    public async Task AddAsync(Event @event)
    {
        await _dbContext.Events.AddAsync(@event);
    }
    public Task UpdateAsync(Event @event)
    {
        _dbContext.Events.Update(@event);
        return Task.CompletedTask;
    }

}
