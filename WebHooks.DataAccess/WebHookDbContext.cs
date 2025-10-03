using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebHook.Domain.Models.Orders;
using WebHook.Domain.Models.Users;
using WebHook.Domain.Models.WebHooks;

namespace WebHook.DataAccess;

public sealed class WebHookDbContext :  IdentityDbContext<AppUser>
{
    public WebHookDbContext(DbContextOptions<WebHookDbContext> options) : base(options)
    {
        
    }
  public DbSet<Order>  Orders { get; set; }
  
  public DbSet<WebhookSub>  WebhookSubs { get; set; }
  
  public DbSet<WebHookDeliveryAttempt>  WebHookDeliveryAttemptsDeliveryAttempts { get; set; }
  
  public DbSet<AppUser>  AppUsers { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      
      base.OnModelCreating(modelBuilder);
      
      modelBuilder.Entity<Order>(builder =>
      {
          builder.ToTable("orders");
          builder.HasKey(o => o.Id);
      });

      modelBuilder.Entity<WebhookSub>(builder =>
      {
          builder.ToTable("sub", "webhooks");
          builder.HasKey(o => o.Id);
          builder.HasIndex(x => x.EventType);
      });
      
      modelBuilder.Entity<WebHookDeliveryAttempt>(builder =>
      {
          builder.ToTable("delivery_attempts", "webhooks");
          builder.HasKey(o => o.Id);

          builder.HasOne<WebhookSub>()
              .WithMany()
              .HasForeignKey(d => d.SubscriptionId);
      });

      modelBuilder.Entity<AppUser>(builder =>
      {
          builder.ToTable("users");
      });
  }
}