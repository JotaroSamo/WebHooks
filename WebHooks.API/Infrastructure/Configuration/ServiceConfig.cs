using WebHook.Domain.IService.Ordes;
using WebHook.Domain.IService.WeebHooks;
using WebHooks.BusinessLogic.Service.Orders;
using WebHooks.BusinessLogic.Service.WebHooks;

namespace WebHools.Infrastructure.Configuration;

public static class ServiceConfig
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IWebHookRepository, WebHookRepository>();

        services.AddScoped<IWebHookDeliveryAttemptRepository, WebHookDeliveryAttemptRepository>();

        services.AddScoped<IWebHookDispatcher,WebHookDispatcher>();
    }
}