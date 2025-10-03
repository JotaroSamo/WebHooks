using MassTransit;
using WebHools.Infrastructure.Consumers;

namespace WebHools.Infrastructure.Configuration;

public static class MassTransitConfig
{
    public static void AddMassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(builder =>
        {
            builder.SetKebabCaseEndpointNameFormatter();

            builder.AddConsumer<WebHookConsumer>();
            builder.AddConsumer<WebHookTriggeredConsumer>();
            
            builder.UsingRabbitMq((context, config)
                =>
            {
                config.Host(configuration.GetConnectionString("rabbitmq"));
                
                config.ConfigureEndpoints(context);
            });
            
        });
    }
}