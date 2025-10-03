using Microsoft.AspNetCore.Identity;
using WebHook.DataAccess;
using WebHook.Domain.Models.Users;

namespace WebHools.Infrastructure.Configuration;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<WebHookDbContext>()
            .AddDefaultTokenProviders();
    }
}