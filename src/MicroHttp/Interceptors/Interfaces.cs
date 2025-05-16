namespace MicroHttp.Interceptors;

/// <summary>
/// Interface for intercepting HTTP requests.
/// </summary>
public interface IRequestInterceptor
{
    /// <summary>
    /// Processes the request before it is sent.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <returns>The processed request.</returns>
    Task<HttpRequestMessage> ProcessRequestAsync(HttpRequestMessage request);
}

/// <summary>
/// Interface for intercepting HTTP responses.
/// </summary>
public interface IResponseInterceptor
{
    /// <summary>
    /// Processes the response after it is received.
    /// </summary>
    /// <param name="response">The response to process.</param>
    /// <returns>The processed response.</returns>
    Task<HttpResponseMessage> ProcessResponseAsync(HttpResponseMessage response);
}