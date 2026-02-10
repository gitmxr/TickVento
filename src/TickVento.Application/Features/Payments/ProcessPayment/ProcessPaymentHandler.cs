
using TickVento.Application.Abstractions.Payments;
using TickVento.Application.Abstractions.Persistence;
using TickVento.Application.Common.Exceptions;
using TickVento.Domain.Enums;
using TickVento.Domain.Entities;

namespace TickVento.Application.Features.Payments.ProcessPayment
{
    public class ProcessPaymentHandler(
        IBookingRepository bookingRepository,
        IPaymentRepository paymentRepository,
        IPaymentGateway paymentGateway,
        IUnitOfWork unitOfWork
        )
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IPaymentGateway _paymentGateway = paymentGateway;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PaymentResult> Handle(ProcessPaymentCommand command)
        {
            // Load Booking 
            var booking = await _bookingRepository.GetByIdAsync(command.BookingId)
                ?? throw new BusinessRuleException("Booking not Found.");

            // Validate Booking Status  
            if (booking.Status != BookingStatus.Pending)
                throw new BusinessRuleException($"Payment cannot be processed for {booking.Status} booking status");

            // Check if booking already has a successful payment
            var existingPayment = await _paymentRepository.GetByBookingIdAsync(booking.Id);
            if (existingPayment?.Status == PaymentStatus.Completed)
                throw new BusinessRuleException("Booking is already paid.");
            
            //Validate Seats Are Still Reserved
            bool isAllSeatReserved = booking.Seats.All(seat => seat.Status == SeatStatus.Reserved);
            if (!isAllSeatReserved)
                throw new BusinessRuleException("one or more seats are not available.");

            // Calculate Payment Amount
            var totalAmount = booking.Seats.Sum(seat => 
                booking.Event.GetPriceForCategory(seat.Category));
           
            //Validate amount (sandbox rules)
            if (totalAmount <= 0 || totalAmount > 100_000)
                throw new BusinessRuleException("This payment Cannot be Processed.");
            
            // Begin uow
            await _unitOfWork.BeginAsync();
            
            try
            {
                //Create Payment (Pending)
                var payment = new Payment(
                    totalAmount,
                    booking,
                    command.Method
                );

                //Persist payment 
                await _paymentRepository.AddAsync(payment);

                //Call Sandbox Payment Gateway

                // create request 
                var paymentRequest = new PaymentRequest(
                    booking.Id,
                    totalAmount,
                    command.Method
                );

                PaymentResult result = await _paymentGateway.ChargeAsync(paymentRequest);

                // Store and return payment Result 
                if (result.IsSuccessful)
                {
                    // update Payment status
                    payment.MarkAsSuccessful(result.TransactionId!);

                    // update booking Status
                    booking.ConfirmBooking();
                }
                else
                {
                    //update Payment Status
                    payment.MarkAsFailed(result.FailureReason, result.TransactionId);

                    //update booking status 
                    booking.CancelBooking();
                }

                // persist changes 
                await _paymentRepository.UpdateAsync(payment);
                await _bookingRepository.UpdateAsync(booking);

                // commit uow
                await _unitOfWork.CommitAsync();

                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
