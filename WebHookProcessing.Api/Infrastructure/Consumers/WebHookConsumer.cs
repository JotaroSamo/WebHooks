using MassTransit;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;

namespace WebHookPrcessing.Api.Infrastructure.Consumers;

internal sealed class WebHookConsumer(IWebHookRepository repository) : IConsumer<WebHookDispatched>
{
    public async Task Consume(ConsumeContext<WebHookDispatched> context)
    {
        var message = context.Message;
        
        var subs = await  repository.GetByEventTypeWebhookSubsAsync(message.EventType);

        await context.PublishBatch(subs.Select(sub => new WebHookTriggered(sub.Id,
            sub.EventType,
            sub.WebhookUrl,
            message.Data)));
    }
}