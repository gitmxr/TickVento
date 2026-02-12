
using Microsoft.EntityFrameworkCore;
using TickVento.Application.Abstractions.Persistence;
using TickVento.Infrastructure.Persistence.Data;
using TickVento.Domain.Entities;

namespace TickVento.Infrastructure.Persistence.Repositories;

public class UserRepository(TickVentoDbContext dbContext) : IUserRepository
{
    private readonly TickVentoDbContext _dbContext = dbContext;

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users
            .FindAsync(userId);
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
    public Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        return  Task.CompletedTask;
    }

}