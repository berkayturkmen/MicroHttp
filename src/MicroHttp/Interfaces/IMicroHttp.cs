using MicroHttp.Models;

namespace MicroHttp.Interfaces
{
    /// <summary>
    /// Interface for making HTTP requests in a simple and efficient way.
    /// </summary>
    public interface IMicroHttp
    {
        #region GET Methods

        /// <summary>
        /// Sends an HTTP GET request to the specified URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> GetAsync<T>(string url, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP GET request to the specified URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> GetAsync<T>(string url, RequestContext context);

        /// <summary>
        /// Sends an HTTP GET request to the specified URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask GetAsync(string url, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP GET request to the specified URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask GetAsync(string url, RequestContext context);

        #endregion

        #region POST Methods

        /// <summary>
        /// Sends an HTTP POST request with the specified data to the URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PostAsync<T>(string url, object data, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP POST request with the specified data to the URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PostAsync<T>(string url, object data, RequestContext context);

        /// <summary>
        /// Sends an HTTP POST request with the specified data to the URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask PostAsync(string url, object data, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP POST request with the specified data to the URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask PostAsync(string url, object data, RequestContext context);

        #endregion

        #region PUT Methods

        /// <summary>
        /// Sends an HTTP PUT request with the specified data to the URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PutAsync<T>(string url, object data, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP PUT request with the specified data to the URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PutAsync<T>(string url, object data, RequestContext context);

        /// <summary>
        /// Sends an HTTP PUT request with the specified data to the URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask PutAsync(string url, object data, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP PUT request with the specified data to the URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask PutAsync(string url, object data, RequestContext context);

        #endregion

        #region PATCH Methods

        /// <summary>
        /// Sends an HTTP PATCH request with the specified data to the URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PatchAsync<T>(string url, object data, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP PATCH request with the specified data to the URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PatchAsync<T>(string url, object data, RequestContext context);

        /// <summary>
        /// Sends an HTTP PATCH request with the specified data to the URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask PatchAsync(string url, object data, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP PATCH request with the specified data to the URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="data">The data to send with the request.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask PatchAsync(string url, object data, RequestContext context);

        #endregion

        #region DELETE Methods

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> DeleteAsync<T>(string url, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URL and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> DeleteAsync<T>(string url, RequestContext context);

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask DeleteAsync(string url, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URL without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask DeleteAsync(string url, RequestContext context);

        #endregion

        #region File Upload Methods

        /// <summary>
        /// Sends an HTTP POST request to upload a file and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="fileRequest">The file upload request details.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PostFileAsync<T>(string url, FileUploadRequest fileRequest, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP POST request to upload a file and returns the deserialized response.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response content into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="fileRequest">The file upload request details.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The deserialized response content.</returns>
        ValueTask<T> PostFileAsync<T>(string url, FileUploadRequest fileRequest, RequestContext context);

        /// <summary>
        /// Sends an HTTP POST request to upload a file without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="fileRequest">The file upload request details.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask PostFileAsync(string url, FileUploadRequest fileRequest, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP POST request to upload a file without returning response content.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="fileRequest">The file upload request details.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask PostFileAsync(string url, FileUploadRequest fileRequest, RequestContext context);

        #endregion

        #region Streaming Methods

        /// <summary>
        /// Sends an HTTP GET request to retrieve a stream and processes it using the provided stream processor.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="streamProcessor">The function to process the stream.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        ValueTask GetStreamAsync(string url, Func<Stream, Task> streamProcessor, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP GET request to retrieve a stream and processes it using the provided stream processor.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="streamProcessor">The function to process the stream.</param>
        /// <param name="context">Request context with additional parameters.</param>
        ValueTask GetStreamAsync(string url, Func<Stream, Task> streamProcessor, RequestContext context);

        /// <summary>
        /// Sends an HTTP GET request to retrieve a JSON stream and deserializes it into a list of objects.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON stream into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="clientName">Optional name of the client to use.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>A list of deserialized objects.</returns>
        ValueTask<IList<T>> GetJsonStreamAsync<T>(string url, string? clientName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP GET request to retrieve a JSON stream and deserializes it into a list of objects.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON stream into.</typeparam>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>A list of deserialized objects.</returns>
        ValueTask<IList<T>> GetJsonStreamAsync<T>(string url, RequestContext context);

        #endregion

        #region Batch Methods

        /// <summary>
        /// Executes a batch of requests and returns the results.
        /// </summary>
        /// <param name="batch">The batch of requests to execute.</param>
        /// <param name="cancellationToken">Optional token to cancel the request.</param>
        /// <returns>The results of the batch execution.</returns>
        ValueTask<BatchResults> ExecuteBatchAsync(RequestBatch batch, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a batch of requests and returns the results.
        /// </summary>
        /// <param name="batch">The batch of requests to execute.</param>
        /// <param name="context">Request context with additional parameters.</param>
        /// <returns>The results of the batch execution.</returns>
        ValueTask<BatchResults> ExecuteBatchAsync(RequestBatch batch, RequestContext context);

        #endregion
    }
}
