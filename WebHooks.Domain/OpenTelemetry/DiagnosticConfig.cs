using System.Diagnostics;

namespace WebHook.Domain.OpenTelemetry;

public static class DiagnosticConfig
{
    public static readonly ActivitySource Source = new("webhooks-api");
}