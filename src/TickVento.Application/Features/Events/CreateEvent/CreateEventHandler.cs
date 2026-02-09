using TickVento.Application.Abstractions.Persistence;
using TickVento.Domain.Entities;
using TickVento.Domain.Enums;

namespace TickVento.Application.Features.Events.CreateEvent;

public class CreateEventHandler
{
    private readonly IEventRepository _eventRepository;

    public CreateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<CreateEventResult> Handle(CreateEventCommand command)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        // 1. Create Venue
        var venue = new Venue(
            command.VenueName,
            command.TotalSeats
        );

        // 2. Create Event (aggregate root)
        var newEvent = new Event(
            command.Title,
            command.Description,
            venue,
            command.EventDate
        );

        // 3. Create Seats through Event behavior
        for (int i = 1; i <= command.TotalSeats; i++)
        {
            newEvent.AddSeat(
                seatNumber: i.ToString(),
                category: SeatCategory.Regular
            );
        }

        // 4. Persist aggregate
        await _eventRepository.AddAsync(newEvent);

        return new CreateEventResult(
            newEvent.Id,
            newEvent.Title
        );
    }
}
