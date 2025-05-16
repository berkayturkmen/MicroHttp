namespace MicroHttp.Models;

/// <summary>
/// Represents a batch of requests to be executed.
/// </summary>
public class RequestBatch
{
    private readonly List<BatchRequestItem> _requests = new();
    
    /// <summary>
    /// Adds a GET request to the batch.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
    /// <param name="url">The URL to send the request to.</param>
    /// <param name="context">Optional request context.</param>
    /// <returns>The index of the request in the batch.</returns>
    public int Add<T>(string url, RequestContext? context = null)
    {
        var request = new BatchRequestItem
        {
            Url = url,
            Method = HttpMethod.Get,
            ResponseType = typeof(T),
            Context = context
        };
        
        _requests.Add(request);
        return _requests.Count - 1;
    }
    
    /// <summary>
    /// Adds a POST request to the batch.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
    /// <param name="url">The URL to send the request to.</param>
    /// <param name="data">The data to send with the request.</param>
    /// <param name="context">Optional request context.</param>
    /// <returns>The index of the request in the batch.</returns>
    public int AddPost<T>(string url, object data, RequestContext? context = null)
    {
        var request = new BatchRequestItem
        {
            Url = url,
            Method = HttpMethod.Post,
            Data = data,
            ResponseType = typeof(T),
            Context = context
        };
        
        _requests.Add(request);
        return _requests.Count - 1;
    }
    
    // Additional methods for other HTTP methods...
    
    /// <summary>
    /// Gets the requests in the batch.
    /// </summary>
    public IReadOnlyList<BatchRequestItem> Requests => _requests;
}

/// <summary>
/// Represents an item in a batch request.
/// </summary>
public class BatchRequestItem
{
    /// <summary>
    /// Gets or sets the URL to send the request to.
    /// </summary>
    public string Url { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the HTTP method to use.
    /// </summary>
    public HttpMethod Method { get; set; } = HttpMethod.Get;
    
    /// <summary>
    /// Gets or sets the data to send with the request.
    /// </summary>
    public object? Data { get; set; }
    
    /// <summary>
    /// Gets or sets the type to deserialize the response content into.
    /// </summary>
    public Type ResponseType { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the request context.
    /// </summary>
    public RequestContext? Context { get; set; }
}

/// <summary>
/// Contains the results of a batch request.
/// </summary>
public class BatchResults
{
    private readonly Dictionary<int, object> _results = new();
    
    /// <summary>
    /// Gets a result by index.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="index">The index of the result.</param>
    /// <returns>The result.</returns>
    public T Get<T>(int index)
    {
        if (!_results.TryGetValue(index, out var result))
        {
            throw new KeyNotFoundException($"No result found at index {index}");
        }
        
        return (T)result;
    }
    
    /// <summary>
    /// Sets a result.
    /// </summary>
    /// <param name="index">The index of the result.</param>
    /// <param name="result">The result.</param>
    internal void SetResult(int index, object result)
    {
        _results[index] = result;
    }
    
    /// <summary>
    /// Gets the number of results.
    /// </summary>
    public int Count => _results.Count;
}