namespace WebHook.Domain.Models.WebHooks;

public sealed record WebHookTriggered(Guid SubId, string EventType, string WebhookUrl, object Data);