using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.WebHooks;

namespace WebHools.Endpoints.WebHooks;

public static class WebHookEndPoints
{
    public static void MapWebHookEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/webhook");
        
        group.MapPost("/sub", Sub);
     
    }
    
    private static async Task<IResult> Sub(CreateWebhookRequest request, IWebHookRepository repository)
    {
        var webhook = new WebhookSub(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);
    
        await repository.CreateWebhookSubAsync(webhook);
    
        return Results.Ok(webhook);
    }

}