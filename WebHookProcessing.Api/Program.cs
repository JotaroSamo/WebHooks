


using Npgsql;
using WebHookPrcessing.Api.Infrastructure.Configuration;
using WebHookPrcessing.Api.Infrastructure.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddDataBase(builder.Configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.AddMassTransitConfig(builder.Configuration);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing.AddSource(DiagnosticConfig.Source.Name)
            .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName)
            .AddNpgsql();
    });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

