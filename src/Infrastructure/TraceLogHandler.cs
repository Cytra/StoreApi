using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class TraceLogHandler : DelegatingHandler
{
    private readonly ILogger<TraceLogHandler> _logger;
    private readonly Func<HttpResponseMessage, bool> _shouldLog;

    private static readonly string[] headersToAnonymise = { "apiKey", "Authorization" };

    public TraceLogHandler(ILogger<TraceLogHandler> logger, Func<HttpResponseMessage, bool> shouldLog)
    {
        _logger = logger;
        _shouldLog = shouldLog;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var logPayloads = false;
        HttpResponseMessage response = null;
        try
        {
            response = await base.SendAsync(request, cancellationToken);
            logPayloads = _shouldLog(response);
        }
        catch (Exception)
        {
            //if there is exception we want to log it.
            logPayloads = true;
            throw;
        }
        finally
        {
            if (logPayloads)
                try
                {
                    var scope = new Dictionary<string, object>();

                    scope.TryAdd("requestHeaders", CollectHeaders(request.Headers));
                    if (request?.Content != null) scope.Add("requestBody", await request.Content.ReadAsStringAsync());
                    scope.TryAdd("responseHeaders", CollectHeaders(response.Headers));
                    if (response?.Content != null)
                        scope.Add("responseBody", await response.Content.ReadAsStringAsync());
                    using (_logger.BeginScope(scope))
                    {
                        _logger.LogWarning("request/response details");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to log http request / response details");
                }
        }

        return response;
    }

    public static string CollectHeaders(HttpHeaders headers)
    {
        if (headers is null) return string.Empty;
        var anonymizedHeaders = headers.Select(Anonymize);
        return JsonSerializer.Serialize(
            anonymizedHeaders.Select(o => $"{o.Key}: {string.Join(",", o.Value)}"));
    }

    public static KeyValuePair<string, IEnumerable<string>> Anonymize(KeyValuePair<string, IEnumerable<string>> header)
    {
        if (headersToAnonymise.Contains(header.Key, StringComparer.OrdinalIgnoreCase))
            return new KeyValuePair<string, IEnumerable<string>>(header.Key, new[] { "Anonymized" });
        return header;
    }
}