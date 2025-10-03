namespace WebHook.Domain.Models.WebHooks;

public sealed record WebhookSub(Guid Id, string EventType, string WebhookUrl, DateTime CreatedOnUtc);

public sealed record CreateWebhookRequest(string EventType, string WebhookUrl);