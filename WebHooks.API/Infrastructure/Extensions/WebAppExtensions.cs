using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebHook.DataAccess;

namespace WebHools.Infrastructure.Extensions;

public static class WebAppExtensions
{
    public static async Task ApplyMigrationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<WebHookDbContext>();

        await db.Database.MigrateAsync();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        string[] roleNames = { "Admin", "User", "Moderator" };
    
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}