using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Entities;

namespace TickVento.Application.Abstractions.Persistence
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(Guid id);
        Task<Event?> GetByIdWithSeatsAsync(Guid id);
        Task AddAsync(Event @event);
        Task UpdateAsync(Event @event);

    }
}
