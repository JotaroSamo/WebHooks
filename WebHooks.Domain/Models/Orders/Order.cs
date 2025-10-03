namespace WebHook.Domain.Models.Orders;

public class Order
{
    public Order()
    {
        
    }
    public Order(Guid orderId, string name, DateTime createdOnUtc)
    {
        Id = orderId;
        Name = name;
        CreatedOnUtc = createdOnUtc;
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime CreatedOnUtc { get; set; }
    
}