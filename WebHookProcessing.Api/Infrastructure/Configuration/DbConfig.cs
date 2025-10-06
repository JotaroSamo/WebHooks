using Microsoft.EntityFrameworkCore;
using WebHook.DataAccess;

namespace WebHookPrcessing.Api.Infrastructure.Configuration;

public static class DbConfig
{
    public static void AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebHookDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("webhooks"),
                config => config.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
                    .CommandTimeout((int) TimeSpan.FromMinutes(2).TotalSeconds));
        });
    }
}