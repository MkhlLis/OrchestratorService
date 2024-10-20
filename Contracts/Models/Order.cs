namespace Orchestrator.Contracts.Models;

public class Order
{
    public int CustomerId { get; set; }
    public string Title { get; set; }
    public int Priority { get; set; }
}