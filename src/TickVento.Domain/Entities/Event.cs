using System;
using System.Collections.Generic;
using System.Linq;
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

        // ✅ New: Seat prices per category
        public Dictionary<SeatCategory, decimal> SeatPrices { get; private set; }

        public Event()
        {
            Seats = new List<Seat>();
            Bookings = new List<Booking>();
            SeatPrices = new Dictionary<SeatCategory, decimal>();
        }

        // Updated constructor
        public Event(string title, string description, Venue venue, DateTime eventDate, Dictionary<SeatCategory, decimal>? seatPrices = null)
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
            Venue = venue ?? throw new ArgumentException("Venue cannot be empty.");
            VenueId = Venue.Id;
            EventDate = eventDate;

            Seats = new List<Seat>();
            Bookings = new List<Booking>();

            // Initialize seat prices
            SeatPrices = seatPrices ?? new Dictionary<SeatCategory, decimal>();
        }

        public void UpdateEventDetail(string title, string description, Venue venue, DateTime eventDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Event title cannot be empty.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event Description cannot be empty.");

            if (eventDate < DateTime.UtcNow)
                throw new ArgumentException("Event date cannot be in the past.");

            Title = title;
            Description = description;
            Venue = venue ?? throw new ArgumentException("Venue cannot be empty.");
            VenueId = Venue.Id;
            EventDate = eventDate;
        }

        public void AddSeat(string seatNumber, SeatCategory category)
        {
            if (Seats.Any(s => s.SeatNumber == seatNumber))
                throw new ArgumentException("SeatNumber already exists!");

            var seat = new Seat(seatNumber, category, this.Id);
            Seats.Add(seat);
        }

        // ✅ New: Get price for a seat category
        public decimal GetPriceForCategory(SeatCategory category)
        {
            if (!SeatPrices.TryGetValue(category, out var price))
                throw new ArgumentException("Seat category is not priced for this event.");

            return price;
        }

        // ✅ New: Add seats automatically based on category distribution
        public void GenerateSeats(Dictionary<SeatCategory, int> categoryCounts)
        {
            foreach (var kvp in categoryCounts)
            {
                var category = kvp.Key;
                var count = kvp.Value;

                for (int i = 1; i <= count; i++)
                {
                    // Example: R1, R2, P1, P2, V1...
                    string seatNumber = $"{category.ToString()[0]}{i}";
                    AddSeat(seatNumber, category);
                }
            }
        }
    }
}
