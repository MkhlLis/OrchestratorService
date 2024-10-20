using Orchestrator.Contracts.Interfaces;
using Orchestrator.Contracts.Models;

namespace Orchestrator.Stores;

/// <summary>
/// Хранилище.
/// </summary>
public class InMemoryStore : IStore
{
    private readonly List<CustomerInfo> _customers = new();
    private readonly List<Order> _orders = new();

    public InMemoryStore()
    {
        _customers.AddRange(new List<CustomerInfo>
        {
            new CustomerInfo {Id = 1, FirstName = "John", LastName = "Doe"},
            new CustomerInfo {Id = 2, FirstName = "Jane", LastName = "Doe"},
            new CustomerInfo {Id = 3, FirstName = "Michael", LastName = "Doe"},
            new CustomerInfo {Id = 4, FirstName = "Ivan", LastName = "Petrov"},
            new CustomerInfo {Id = 5, FirstName = "Natali", LastName = "Petrova"},
        });
    }
    
    public async Task AddNewOrderAsync(OrderBase orderBase, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        _orders.Add(new Order
        {
            CustomerId = orderBase.CustomerId,
            Title = orderBase.Title,
            Priority = 0,
        });
    }

    public async Task<IEnumerable<BookingRequest>> GetAllRequests(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        var bookingRequests = new List<BookingRequest>();
        var titles = _orders.Select(x => x.Title).Distinct();
        foreach (var title in titles)
        {
            bookingRequests.Add(new BookingRequest
            {
                Title = title,
                Customers = _orders
                    .Where(x => x.Title == title)
                    .OrderBy(x => x.Priority)
                    .Join(_customers,
                    order => order.CustomerId,
                    customer => customer.Id,
                    (_, customer) => customer).ToList(),
            });
        }
        return bookingRequests;
    }

    public async Task OrderEvent(OrderBase orderBase, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        _orders.Single(x => x.Title == orderBase.Title && x.CustomerId == orderBase.CustomerId).Priority += 1;
    }
}