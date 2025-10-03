namespace WebHook.Domain.Models.WebHooks;

public record WebhookPayload(Guid Id, string EventType, Guid Subscription, DateTime Timestamp, object Data);