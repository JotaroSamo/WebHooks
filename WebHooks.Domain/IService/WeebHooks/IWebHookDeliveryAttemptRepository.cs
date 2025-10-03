using WebHook.Domain.Models.WebHooks;

namespace WebHook.Domain.IService.WeebHooks;

public interface IWebHookDeliveryAttemptRepository
{
    Task<WebHookDeliveryAttempt> Create(WebHookDeliveryAttempt webHookDeliveryAttempt);
}
