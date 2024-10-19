namespace Orchestrator.Contracts.Models;

/// <summary>
/// Заказ.
/// </summary>
public class Order
{
    public int CustomerId { get; set; }
    public string Title { get; set; }
}