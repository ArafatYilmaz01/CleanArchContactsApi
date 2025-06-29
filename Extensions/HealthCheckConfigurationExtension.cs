using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BP.Extensions
{
    public static class HealthCheckConfigurationExtension
    {
public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app)
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/api/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        description = entry.Value.Description
                    })
                });
                await context.Response.WriteAsync(result);
            }
        });
    });
    return app;
}
    }
}