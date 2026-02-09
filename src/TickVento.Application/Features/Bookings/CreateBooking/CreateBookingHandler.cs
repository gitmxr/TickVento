
using TickVento.Application.Abstractions.Persistence;
using TickVento.Application.Common.Exceptions;
using TickVento.Domain.Entities;

namespace TickVento.Application.Features.Bookings.CreateBooking
{
    public class CreateBookingHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBookingRepository _bookingRepository;
        public CreateBookingHandler(
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IBookingRepository bookingRepository
            )
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<CreateBookingResult> Handle(CreateBookingCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                throw new BusinessRuleException("User is not found");

            var @event = await _eventRepository.GetByIdWithSeatsAsync(command.EventId);
            if (@event == null)
                throw new BusinessRuleException("Event is not Found");

            // User Selected Seats
            var selectedSeatNumbers = command.SeatNumbers;
            
            // Fetch all seats from the event
            var eventSeats = @event.Seats;

            // Ensure all requested seats exist in the event
            bool allSeatsExist = selectedSeatNumbers.All(seatNumber =>
                eventSeats.Any(seat => seat.SeatNumber == seatNumber)
            );

            if (!allSeatsExist)
                throw new BusinessRuleException("One or more selected seats do not exist.");


            //From all event seats, pick only those seats whose SeatNumber exists in the list selected by the user

            // No linq method
            //var selectedSeats = new List<Seat>();
            //foreach (var seat in eventSeats)
            //{
            //    if (selectedSeatNumbers.Contains(seat.SeatNumber))
            //    {
            //        selectedSeats.Add(seat);
            //    }
            //}
            
            var selectedSeats = eventSeats
               .Where(seat => selectedSeatNumbers.Contains(seat.SeatNumber))
               .ToList();
            
            //Check all Selected Seats Available 
            bool allSeatAvailable = selectedSeats.All(seat => seat.Status == Domain.Enums.SeatStatus.Available);
            
            if(!allSeatAvailable)
                throw new BusinessRuleException("One or more selected seats are not available.");
            
            // Create a new Booking 
            var booking = new Booking(
                user,
                @event,
                selectedSeats
            );
            
            // save Booking 
            await _bookingRepository.AddAsync(booking);

            // retun results 
            return new CreateBookingResult(
                booking.Id,
                booking.Event.Title,
                selectedSeats.Select(s => s.SeatNumber)
            );
        }
    }
}
