using WebHook.Domain.Models.WebHooks;

namespace WebHook.Domain.IService.WeebHooks;

public interface IWebHookRepository
{
    Task<WebhookSub> CreateWebhookSubAsync(WebhookSub webhookSub);
    public Task<IReadOnlyList<WebhookSub>> GetByEventTypeWebhookSubsAsync(string eventType);
}
