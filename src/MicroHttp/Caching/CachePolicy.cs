namespace MicroHttp.Caching;

/// <summary>
/// Defines caching behavior for HTTP requests.
/// </summary>
public class CachePolicy
{
    /// <summary>
    /// Gets or sets the duration to cache the response.
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(5);
    
    /// <summary>
    /// Gets or sets whether to use sliding expiration.
    /// </summary>
    public bool SlidingExpiration { get; set; } = false;
    
    /// <summary>
    /// Gets or sets whether to force refresh the cache.
    /// </summary>
    public bool ForceRefresh { get; set; } = false;
    
    /// <summary>
    /// Gets or sets custom cache key parts to include in the cache key.
    /// </summary>
    public string[]? CustomCacheKeyParts { get; set; }
    
    /// <summary>
    /// Gets or sets whether to respect cache control headers.
    /// </summary>
    public bool RespectCacheControlHeaders { get; set; } = true;
    
    /// <summary>
    /// Creates a new instance with default values.
    /// </summary>
    public static CachePolicy Default => new();
    
    /// <summary>
    /// Creates a new instance with no caching.
    /// </summary>
    public static CachePolicy NoCache => new() { Duration = TimeSpan.Zero, RespectCacheControlHeaders = false };
}