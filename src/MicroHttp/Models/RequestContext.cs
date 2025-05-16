using MicroHttp.Caching;
using MicroHttp.Interceptors;

namespace MicroHttp.Models;

/// <summary>
/// Contains context information for HTTP requests.
/// </summary>
public class RequestContext
{
    /// <summary>
    /// Gets or sets the name of the client to use.
    /// </summary>
    public string? ClientName { get; set; }
    
    /// <summary>
    /// Gets or sets the HTTP headers to include with the request.
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
    
    /// <summary>
    /// Gets or sets the cancellation token to cancel the request.
    /// </summary>
    public CancellationToken CancellationToken { get; set; } = default;
    
    /// <summary>
    /// Gets or sets the caching policy for the request.
    /// </summary>
    public CachePolicy? CachePolicy { get; set; }
    
    /// <summary>
    /// Gets or sets a list of request interceptors to execute for this request.
    /// </summary>
    public List<IRequestInterceptor>? RequestInterceptors { get; set; }
    
    /// <summary>
    /// Gets or sets a list of response interceptors to execute for this request.
    /// </summary>
    public List<IResponseInterceptor>? ResponseInterceptors { get; set; }
    
    /// <summary>
    /// Creates a new instance with default values.
    /// </summary>
    public static RequestContext Default => new();
    
    /// <summary>
    /// Creates a new builder for fluent construction.
    /// </summary>
    public static RequestContextBuilder CreateBuilder() => new();
}

/// <summary>
/// Fluent builder for request context.
/// </summary>
public class RequestContextBuilder
{
    private readonly RequestContext _context = new();
    
    public RequestContextBuilder WithClient(string clientName)
    {
        _context.ClientName = clientName;
        return this;
    }
    
    public RequestContextBuilder WithHeader(string key, string value)
    {
        _context.Headers ??= new Dictionary<string, string>();
        _context.Headers[key] = value;
        return this;
    }
    
    public RequestContextBuilder WithHeaders(Dictionary<string, string> headers)
    {
        _context.Headers = headers;
        return this;
    }
    
    public RequestContextBuilder WithCancellation(CancellationToken token)
    {
        _context.CancellationToken = token;
        return this;
    }
    
    public RequestContextBuilder WithCachePolicy(CachePolicy policy)
    {
        _context.CachePolicy = policy;
        return this;
    }
    
    public RequestContext Build() => _context;
}