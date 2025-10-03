namespace WebHook.Domain.Models.WebHooks;

public record WebhookPayload<T>(Guid Id, string EventType, Guid Subscription, DateTime Timestamp, T Data);