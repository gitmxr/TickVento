using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Enums;

namespace TickVento.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; } 
        public DateTime EventDate { get; private set; }
        public Guid VenueId { get; private set; }
        public Venue Venue { get; private set; }
        public ICollection<Seat> Seats { get; private set; }
        public ICollection<Booking> Bookings { get; private set; }

        public Event() { }  
        public Event(string title, string description,Venue venue, DateTime eventDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Event title cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event Description cannot be empty.");

            if (eventDate < DateTime.UtcNow)
                throw new ArgumentException("Event date cannot be in the past.");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Venue = venue ?? throw new ArgumentException("Venue cannot be in the empty.");
            VenueId = Venue.Id;
            EventDate = eventDate;
            Seats = new List<Seat>();
            Bookings = new List<Booking>();
        }

        public void UpdateEventDetail(string title, string description,Venue venue, DateTime eventDate)
        {

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Event title cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event Description cannot be empty.");

            if (eventDate < DateTime.UtcNow)
                throw new ArgumentException("Event date cannot be in the past.");

            Title = title;
            Description = description;
            Venue = venue ?? throw new ArgumentException("Venue cannot be in the empty.");
            VenueId = Venue.Id;
            EventDate = eventDate;

        }
        public void AddSeat(string seatNumber, SeatCategory category)
        {
            // check if already exist 
            if(Seats.Any(s => s.SeatNumber == seatNumber))
                throw new ArgumentException("SeatNumber is already exist!");

            // create new seat 
            var seat = new Seat(seatNumber, category, this.Id);

            //Add to collection 
            Seats.Add(seat);
        }

       
    }
}
 