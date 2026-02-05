using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Entities;

namespace TickVento.Application.Abstractions.Persistence
{
    public interface IBookingRepository
    {
        Task<Booking?> GetAsync(Guid id);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);  

    }
}
