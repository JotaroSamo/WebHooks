namespace WebHook.Domain.Models.WebHooks;

public class WebHookDeliveryAttempt
{
    public Guid Id { get; set; }
    
    public Guid SubscriptionId { get; set; }
    
    public string Payload { get; set; }
    
    public int? ResponseStatusCode { get; set; }

    public bool IsSuccess {get; set; } 
    
    public DateTime Timestamp { get; set; }
}