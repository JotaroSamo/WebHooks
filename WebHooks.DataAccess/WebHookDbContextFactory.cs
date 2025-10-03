using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebHook.DataAccess;

public class WebHookDbContextFactory : IDesignTimeDbContextFactory<WebHookDbContext>
{
    public WebHookDbContext CreateDbContext(string[] args)
    {
           
        var connection = "Host=localhost;Port=5432;Database=MicroAuthasd;Username=postgres;Password=postgres";
        var optionsBuilder = new DbContextOptionsBuilder<WebHookDbContext>();

        optionsBuilder.UseNpgsql(connection, opt => opt.CommandTimeout((int) TimeSpan.FromMinutes(2).TotalSeconds));

        return new WebHookDbContext(optionsBuilder.Options);
    }
}