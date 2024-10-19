namespace Orchestrator.Contracts.Models;

public class BookingRequestEvent
{
    public string Title { get; set; }
    public IList<Customer> CustomersId { get; set; }
}