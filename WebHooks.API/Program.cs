using AspireWebHook;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebHook.DataAccess;
using WebHook.Domain.IService.Ordes;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.Orders;
using WebHook.Domain.Models.Users;
using WebHook.Domain.Models.WebHooks;
using WebHooks.BusinessLogic.Service.Orders;
using WebHooks.BusinessLogic.Service.WebHooks;
using WebHools.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddServiceDefaults();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IWebHookRepository, WebHookRepository>();

builder.Services.AddScoped<IWebHookDeliveryAttemptRepository, WebHookDeliveryAttemptRepository>();

builder.Services.AddScoped<WebHookDispatcher>();

builder.Services.AddDbContext<WebHookDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("webhooks"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<WebHookDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

await app.ApplyMigrationAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapPost("/webhooks/sub", async (CreateWebhookRequest request, IWebHookRepository repository) =>
{
    var webhook = new WebhookSub(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);
    
    await repository.CreateWebhookSubAsync(webhook);
    
    return Results.Ok(webhook);
});

app.MapPost("/orders", async (CreateOrderRequest request, IOrderRepository orderRepository
    , WebHookDispatcher dispatcher) =>
{
    var order = new Order(Guid.NewGuid(), request.Name, DateTime.UtcNow);
    
    await orderRepository.CreateOrderAsync(order);
    
    await dispatcher.DispatchAsync("order.create", order);
    
    return Results.Ok(order);
    
})
.WithTags("Orders");

app.MapGet("/orders", async (IOrderRepository orderRepository) =>
{
    return Results.Ok(await orderRepository.GetAllOrdersAsync());
    
}).WithTags("Orders");

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.Run();

