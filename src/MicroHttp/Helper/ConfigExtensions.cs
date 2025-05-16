using MicroHttp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroHttp.Helper;

/// <summary>
/// Extension methods for registering MicroHttp services.
/// </summary>
public static class ConfigExtensions
{
    /// <summary>
    /// Adds MicroHttp services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configureClient">Optional action to further configure the HTTP client.</param>
    /// <param name="configureJson">Optional action to configure the JSON serialization options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddMicroHttp(
        this IServiceCollection services,
        Action<IHttpClientBuilder>? configureClient = null,
        Action<JsonSerializerOptions>? configureJson = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        // Add default HttpClient if not already added
        services.AddHttpClient();
        
        // Configure default client if requested
        var clientBuilder = services.AddHttpClient(string.Empty, client =>
        {
            // Set reasonable defaults
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("br"));
            client.DefaultRequestVersion = new Version(3, 0); // HTTP/3
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
        });
        
        // Apply custom configuration if provided
        configureClient?.Invoke(clientBuilder);
        
        // Configure JSON options
        if (configureJson != null)
        {
            services.Configure<JsonSerializerOptions>(options =>
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.PropertyNameCaseInsensitive = true;
                
                // Apply custom JSON configuration
                configureJson(options);
            });
        }
        
        // Register the MicroHttp service
        services.AddSingleton<IMicroHttp, MicroHttp>();
        
        return services;
    }
}
