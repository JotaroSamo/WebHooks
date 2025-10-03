using System.Net.Http.Json;
using System.Text.Json;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;

namespace WebHooks.BusinessLogic.Service.WebHooks;

public sealed class WebHookDispatcher(IHttpClientFactory httpClientFactory,
    IWebHookRepository repository,
    IWebHookDeliveryAttemptRepository repositoryDeliveryAttempt)
{
    public async Task DispatchAsync<T>(string eventType, T data)
    {
        var webHookSubs = await repository.GetByEventTypeWebhookSubsAsync(eventType);

        foreach (var webHookSub in webHookSubs)
        {
            using var httpClient = httpClientFactory.CreateClient();

            var payload = new WebhookPayload<T>(Guid.NewGuid(),
                webHookSub.EventType,
                webHookSub.Id,
                DateTime.UtcNow,
                data);
            
            var jsonPayload = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsJsonAsync(webHookSub.WebhookUrl, payload);

                var attempt = new WebHookDeliveryAttempt()
                {
                    Id = Guid.NewGuid(),
                    SubscriptionId = webHookSub.Id,
                    Payload = jsonPayload,
                    ResponseStatusCode = (int)response.StatusCode,
                    IsSuccess = response.IsSuccessStatusCode,
                    Timestamp = DateTime.UtcNow
                };

                await repositoryDeliveryAttempt.Create(attempt);

            }
            catch (Exception e)
            {
                var attempt = new WebHookDeliveryAttempt()
                {
                    Id = Guid.NewGuid(),
                    SubscriptionId = webHookSub.Id,
                    Payload = jsonPayload,
                    ResponseStatusCode = 0,
                    IsSuccess = false,
                    Timestamp = DateTime.UtcNow
                };

                await repositoryDeliveryAttempt.Create(attempt);
            }
            
        }
        
        
    }
}