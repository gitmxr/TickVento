using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Enums;

namespace TickVento.Domain.Entities
{
    public class Booking
    {
      public Guid Id { get; private set; }   
      public User User { get; private set; }
      public Guid UserId { get; private set; }
      public Event Event { get; private set; }
      public Guid EventId { get; private set; }
      public DateTime BookingDate { get; private set; }
      public BookingStatus Status { get; private set; }
      public ICollection<Seat> Seats { get; private set; }
      public Booking() { } 
      public Booking(User user, Event @event, ICollection<Seat> seats)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(@event);

            if (seats == null || !seats.Any())
                throw new ArgumentException("Booking must contain at least one seat.");

            Id = Guid.NewGuid();
            BookingDate = DateTime.UtcNow;
            Status = BookingStatus.Pending;

            User = user;
            UserId = user.Id;

            Event = @event;
            EventId = @event.Id;

            Seats = seats.ToList();

            if (Seats.Any(s => s.EventId != @event.Id))
                throw new ArgumentException("All seats must belong to the same event.");

            if (Seats.Any(s => s.Status != SeatStatus.Available))
                throw new ArgumentException("One or more seats are not available.");

            foreach (var seat in Seats)
                seat.Reserve();
        }
        public void ConfirmBooking()
        {
            if (Status != BookingStatus.Pending)
                throw new ArgumentException("Booking cannot confirm wihout Payment!");
            foreach (var seat in Seats)
                seat.Book();
            Status = BookingStatus.Confirmed;
        }
        public void CancelBooking()
        {
            if (Status == BookingStatus.Cancelled)
                return;
            foreach (var seat in Seats)
                seat.Release();
            Status = BookingStatus.Cancelled;
        }
    }
}
 