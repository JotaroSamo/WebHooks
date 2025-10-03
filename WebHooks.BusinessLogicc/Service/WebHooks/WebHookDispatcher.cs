using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Channels;
using MassTransit;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;
using WebHook.Domain.OpenTelemetry;

namespace WebHooks.BusinessLogic.Service.WebHooks;

public sealed class WebHookDispatcher(
    IPublishEndpoint publishEndpoint) : IWebHookDispatcher
{
    public async Task DispatchAsync<T>(string eventType, T data) where T : notnull
    {
        using Activity? activity = DiagnosticConfig.Source.StartActivity($"{eventType} dispatch webhook");
        activity?.AddTag("event.type", eventType);
        await publishEndpoint.Publish(new WebHookDispatched(eventType, data));
    }
    
}
