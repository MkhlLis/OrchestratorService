using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;

namespace Orchestrator.Mappers;

internal class EventMapper : IEventMapper<BookingRequest, BookingRequestEvent>
{
    public BookingRequestEvent Map(BookingRequest source)
    {
        return new BookingRequestEvent
        {
            Title = source.Title,
            Customers = source.Customers.Select(c => new Customer { CustomerId = c.Id }).ToList(),
        };
    }

    public BookingRequest Map(BookingRequestEvent destination)
    {
        throw new NotImplementedException();
    }
}