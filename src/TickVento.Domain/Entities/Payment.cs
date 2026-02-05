using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickVento.Domain.Enums;

namespace TickVento.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Booking Booking { get; private set; }
        public Guid BookingId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaidAt { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }

        public Payment() { }

        public Payment(decimal amount,Booking booking,PaymentMethod method)
        {
            ArgumentNullException.ThrowIfNull(booking);

            if (amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");
            
            Id = Guid.NewGuid();

            Booking = booking;
            BookingId = booking.Id;

            Amount = amount;
            Method = method;
            Status = PaymentStatus.Pending;
        }
        public void MarkAsSuccessful()
        {
            if(Status != PaymentStatus.Pending)
                throw new InvalidOperationException("Payment already processed.");

            Status = PaymentStatus.Completed;
            PaidAt = DateTime.UtcNow;
        }
        public void MarkAsFailed() 
        {
            if (Status != PaymentStatus.Pending)
                return;

            Status = PaymentStatus.Failled;
            PaidAt = DateTime.UtcNow;
        }
    }
}
