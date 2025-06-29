using System.Diagnostics;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
            stopwatch.Stop();

            // Başarılı istekler için loglama
            if (context.Response.StatusCode == 200)
            {
                _logger.LogInformation($"Başarılı İstek: {context.Request.Method} {context.Request.Path} ({stopwatch.ElapsedMilliseconds} ms)");
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, $"Hata Oluştu: {context.Request.Method} {context.Request.Path} ({stopwatch.ElapsedMilliseconds} ms)");
            throw;
        }
    }
}
