namespace WebHook.Domain.Models.WebHooks;

public sealed record WebHookDispatched(string EventType, object Data);