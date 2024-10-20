namespace Orchestrator.Contracts.Models;

/// <summary>
/// Заказ.
/// </summary>
public class OrderBase
{
    public int CustomerId { get; set; }
    public string Title { get; set; }
}