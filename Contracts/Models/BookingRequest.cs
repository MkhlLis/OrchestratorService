namespace Orchestrator.Contracts.Models;

/// <summary>
/// Заявка на бронирование товара.
/// </summary>
public class BookingRequest
{
    public string Title { get; set; }
    public IList<CustomerInfo> Customers { get; set; }
}