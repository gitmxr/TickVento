using TickVento.Application.Abstractions.Persistence;
using TickVento.Application.Features.Events.CreateEvent;
using TickVento.Domain.Entities;

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

        //  Calculate total capacity from category counts
        var totalCapacity = command.SeatCategoryCounts.Values.Sum();

        //  Create Venue
        var venue = new Venue(command.VenueName, totalCapacity);

        //  Create Event (aggregate root)
        var newEvent = new Event(
            command.Title,
            command.Description,
            venue,
            command.EventDate,
            command.SeatPrices
        );

        //  Generate seats per category
        foreach (var categoryCount in command.SeatCategoryCounts)
        {
            var category = categoryCount.Key;
            var count = categoryCount.Value;

            for (int i = 1; i <= count; i++)
            {
                string seatNumber = $"{category.ToString().Substring(0, 1).ToUpper()}{i}";
                newEvent.AddSeat(seatNumber, category);
            }
        }

        //  Persist aggregate
        await _eventRepository.AddAsync(newEvent);

        //  Return result including seats per category
        return new CreateEventResult(
            newEvent.Id,
            newEvent.Title,
            command.SeatCategoryCounts
        );
    }
}
