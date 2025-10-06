using System.Diagnostics;

namespace WebHookPrcessing.Api.Infrastructure.OpenTelemetry;

public static class DiagnosticConfig
{
    public static readonly ActivitySource Source = new("webhooks-processing");
}