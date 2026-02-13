
using TickVento.Domain.Enums;

namespace TickVento.Domain.Entities;

public class EventSeatPrice
{
    public Guid Id { get; private set; }
    public Guid EventId { get; private set; }
    public Event Event { get; private set; }
    public SeatCategory Category { get; private set; }
    public decimal Price { get; private set; }

    private EventSeatPrice() { } // For EF

    public EventSeatPrice(Event @event, SeatCategory category, decimal price)
    {
        Id = Guid.NewGuid();
        Event = @event ?? throw new ArgumentNullException(nameof(@event));
        EventId = @event.Id;
        Category = category;
        Price = price;
    }
}

