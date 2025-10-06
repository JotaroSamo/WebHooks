using System.Text.Json;
using MassTransit;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;

namespace WebHookPrcessing.Api.Infrastructure.Consumers;

internal sealed class WebHookTriggeredConsumer(IHttpClientFactory httpClientFactory,
    IWebHookDeliveryAttemptRepository repository) : IConsumer<WebHookTriggered>
{
    public async Task Consume(ConsumeContext<WebHookTriggered> context)
    {
        using var httpClient = httpClientFactory.CreateClient();
        
        var message = context.Message;

        var payload = new WebhookPayload(Guid.NewGuid(),
            message.EventType,
            message.SubId,
            DateTime.UtcNow,
            message.Data);
            
        var jsonPayload = JsonSerializer.Serialize(payload);

        try
        {
            var response = await httpClient.PostAsJsonAsync(message.WebhookUrl, payload);
            
            response.EnsureSuccessStatusCode();

            var attempt = new WebHookDeliveryAttempt()
            {
                Id = Guid.NewGuid(),
                SubscriptionId = message.SubId,
                Payload = jsonPayload,
                ResponseStatusCode = (int)response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode,
                Timestamp = DateTime.UtcNow
            };

            await repository.Create(attempt);

        }
        catch (Exception e)
        {
            var attempt = new WebHookDeliveryAttempt()
            {
                Id = Guid.NewGuid(),
                SubscriptionId = message.SubId,
                Payload = jsonPayload,
                ResponseStatusCode = 0,
                IsSuccess = false,
                Timestamp = DateTime.UtcNow
            };

            await repository.Create(attempt);
        }
    }
}