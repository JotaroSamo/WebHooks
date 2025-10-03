using System.Threading.Channels;
using AspireWebHook;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Scalar.AspNetCore;
using WebHook.DataAccess;
using WebHook.Domain.IService.Ordes;
using WebHook.Domain.IService.WeebHooks;
using WebHook.Domain.Models.Orders;
using WebHook.Domain.Models.Users;
using WebHook.Domain.Models.WebHooks;
using WebHook.Domain.OpenTelemetry;
using WebHooks.BusinessLogic.Service.Orders;
using WebHooks.BusinessLogic.Service.WebHooks;
using WebHools.Endpoints.Orders;
using WebHools.Endpoints.WebHooks;
using WebHools.Infrastructure.Configuration;
using WebHools.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddServiceDefaults();

builder.Services.AddDataBase(builder.Configuration);

builder.Services.AddIdentityConfiguration();

builder.Services.AddServices();

builder.Services.AddMassTransitConfig(builder.Configuration);


builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddSource(DiagnosticConfig.Source.Name)
            .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName)
            .AddNpgsql();
    });

var app = builder.Build();

await app.ApplyMigrationAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapOrderEndpoints();

app.MapWebHookEndpoints();

// app.UseAuthentication();
//
// app.UseAuthorization();

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.Run();

