using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Enums;

namespace TickVento.Domain.Entities
{
    public class Seat
    {
        public Guid Id { get; private set; }
        public Event Event { get; private set; }
        public Guid EventId  { get; private set; }
        public string SeatNumber { get; private set; }
        public SeatCategory Category { get; private set; }
        public SeatStatus Status { get; private set; }
        public Seat() { }
        public Seat(string seatNumber, SeatCategory category , Guid eventId)
        {
            if (string.IsNullOrWhiteSpace(seatNumber))
                throw new ArgumentException("SeatNumber cannot be empty.");

            Id = Guid.NewGuid();
            EventId = eventId;
            SeatNumber = seatNumber;
            Category = category; 
            Status = SeatStatus.Available;        
        }
        public void Reserve()
        { 
            if(Status == SeatStatus.Available)
                Status = SeatStatus.Reserved;
        }  
        public void Book() 
        {
            if (Status == SeatStatus.Reserved)
                Status = SeatStatus.Booked;
        }     
        public void Release() 
        {
            if (Status == SeatStatus.Reserved)
                Status = SeatStatus.Available;
        }
    }
}
