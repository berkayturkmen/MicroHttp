using MicroHttp.Interfaces;
using MicroHttp.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MicroHttp;

/// <summary>
/// High-performance HTTP client wrapper optimized for .NET 9.
/// </summary>
public sealed class MicroHttp : IMicroHttp
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Initializes a new instance of the MicroHttp client.
    /// </summary>
    /// <param name="clientFactory">The HTTP client factory to create clients.</param>
    public MicroHttp(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
    }

    #region GET Methods

    /// <inheritdoc/>
    public ValueTask<T> GetAsync<T>(string url, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return GetAsync<T>(url, context);
    }

    /// <inheritdoc/>
    public ValueTask<T> GetAsync<T>(string url, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        return SendAsync<T>(request, context);
    }

    /// <inheritdoc/>
    public ValueTask GetAsync(string url, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return GetAsync(url, context);
    }

    /// <inheritdoc/>
    public ValueTask GetAsync(string url, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        return SendVoidAsync(request, context);
    }

    #endregion

    #region POST Methods

    /// <inheritdoc/>
    public ValueTask<T> PostAsync<T>(string url, object data, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PostAsync<T>(url, data, context);
    }

    /// <inheritdoc/>
    public ValueTask<T> PostAsync<T>(string url, object data, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = CreateJsonRequest(url, HttpMethod.Post, data);
        return SendAsync<T>(request, context);
    }

    /// <inheritdoc/>
    public ValueTask PostAsync(string url, object data, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PostAsync(url, data, context);
    }

    /// <inheritdoc/>
    public ValueTask PostAsync(string url, object data, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = CreateJsonRequest(url, HttpMethod.Post, data);
        return SendVoidAsync(request, context);
    }

    #endregion

    #region PUT Methods

    /// <inheritdoc/>
    public ValueTask<T> PutAsync<T>(string url, object data, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PutAsync<T>(url, data, context);
    }

    /// <inheritdoc/>
    public ValueTask<T> PutAsync<T>(string url, object data, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = CreateJsonRequest(url, HttpMethod.Put, data);
        return SendAsync<T>(request, context);
    }

    /// <inheritdoc/>
    public ValueTask PutAsync(string url, object data, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PutAsync(url, data, context);
    }

    /// <inheritdoc/>
    public ValueTask PutAsync(string url, object data, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = CreateJsonRequest(url, HttpMethod.Put, data);
        return SendVoidAsync(request, context);
    }

    #endregion

    #region PATCH Methods

    /// <inheritdoc/>
    public ValueTask<T> PatchAsync<T>(string url, object data, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PatchAsync<T>(url, data, context);
    }

    /// <inheritdoc/>
    public ValueTask<T> PatchAsync<T>(string url, object data, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = CreateJsonRequest(url, HttpMethod.Patch, data);
        return SendAsync<T>(request, context);
    }

    /// <inheritdoc/>
    public ValueTask PatchAsync(string url, object data, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PatchAsync(url, data, context);
    }

    /// <inheritdoc/>
    public ValueTask PatchAsync(string url, object data, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = CreateJsonRequest(url, HttpMethod.Patch, data);
        return SendVoidAsync(request, context);
    }

    #endregion

    #region DELETE Methods

    /// <inheritdoc/>
    public ValueTask<T> DeleteAsync<T>(string url, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return DeleteAsync<T>(url, context);
    }

    /// <inheritdoc/>
    public ValueTask<T> DeleteAsync<T>(string url, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = new HttpRequestMessage(HttpMethod.Delete, url);
        return SendAsync<T>(request, context);
    }

    /// <inheritdoc/>
    public ValueTask DeleteAsync(string url, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return DeleteAsync(url, context);
    }

    /// <inheritdoc/>
    public ValueTask DeleteAsync(string url, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = new HttpRequestMessage(HttpMethod.Delete, url);
        return SendVoidAsync(request, context);
    }

    #endregion

    #region File Upload Methods

    /// <inheritdoc/>
    public ValueTask<T> PostFileAsync<T>(string url, FileUploadRequest fileRequest, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PostFileAsync<T>(url, fileRequest, context);
    }

    /// <inheritdoc/>
    public async ValueTask<T> PostFileAsync<T>(string url, FileUploadRequest fileRequest, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        using var content = new MultipartFormDataContent();
        
        // Add the main file
        using var fileContent = new StreamContent(fileRequest.File.Content);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileRequest.File.ContentType);
        content.Add(fileContent, fileRequest.File.FieldName, fileRequest.File.FileName);
        
        // Add additional files
        if (fileRequest.AdditionalFiles != null)
        {
            foreach (var file in fileRequest.AdditionalFiles)
            {
                using var additionalFileContent = new StreamContent(file.Content);
                additionalFileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(additionalFileContent, file.FieldName, file.FileName);
            }
        }
        
        // Add form fields
        if (fileRequest.FormFields != null)
        {
            foreach (var field in fileRequest.FormFields)
            {
                content.Add(new StringContent(field.Value), field.Key);
            }
        }
        
        request.Content = content;
        
        return await SendAsync<T>(request, context);
    }

    /// <inheritdoc/>
    public ValueTask PostFileAsync(string url, FileUploadRequest fileRequest, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return PostFileAsync(url, fileRequest, context);
    }

    /// <inheritdoc/>
    public async ValueTask PostFileAsync(string url, FileUploadRequest fileRequest, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        using var content = new MultipartFormDataContent();
        
        // Add the main file
        using var fileContent = new StreamContent(fileRequest.File.Content);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileRequest.File.ContentType);
        content.Add(fileContent, fileRequest.File.FieldName, fileRequest.File.FileName);
        
        // Add additional files
        if (fileRequest.AdditionalFiles != null)
        {
            foreach (var file in fileRequest.AdditionalFiles)
            {
                using var additionalFileContent = new StreamContent(file.Content);
                additionalFileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(additionalFileContent, file.FieldName, file.FileName);
            }
        }
        
        // Add form fields
        if (fileRequest.FormFields != null)
        {
            foreach (var field in fileRequest.FormFields)
            {
                content.Add(new StringContent(field.Value), field.Key);
            }
        }
        
        request.Content = content;
        
        await SendVoidAsync(request, context);
    }

    #endregion

    #region Streaming Methods

    /// <inheritdoc/>
    public ValueTask GetStreamAsync(string url, Func<Stream, Task> streamProcessor, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return GetStreamAsync(url, streamProcessor, context);
    }

    /// <inheritdoc/>
    public async ValueTask GetStreamAsync(string url, Func<Stream, Task> streamProcessor, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (streamProcessor == null) throw new ArgumentNullException(nameof(streamProcessor));
        
        // Create the request without using statement so it won't be disposed prematurely
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        try
        {
            // Add headers
            AddHeaders(request, context.Headers);
            
            var client = _clientFactory.CreateClient(context.ClientName ?? string.Empty);
            
            // Process request interceptors if any
            if (context.RequestInterceptors != null && context.RequestInterceptors.Count > 0)
            {
                foreach (var interceptor in context.RequestInterceptors)
                {
                    request = await interceptor.ProcessRequestAsync(request);
                    // Previous request object could be disposed by interceptor, so we shouldn't use 'using'
                }
            }
            
            // Use a separate variable for response to handle disposal properly
            HttpResponseMessage response = null;
            Stream responseStream = null;
            
            try
            {
                // Send the request
                response = await client.SendAsync(
                    request, 
                    HttpCompletionOption.ResponseHeadersRead, 
                    context.CancellationToken);
                
                // Process response interceptors if any
                if (context.ResponseInterceptors != null && context.ResponseInterceptors.Count > 0)
                {
                    foreach (var interceptor in context.ResponseInterceptors)
                    {
                        var processedResponse = await interceptor.ProcessResponseAsync(response);
                        
                        // If interceptor returns a new response, dispose the old one
                        if (processedResponse != response)
                        {
                            var oldResponse = response;
                            response = processedResponse;
                            oldResponse.Dispose();
                        }
                    }
                }
                
                response.EnsureSuccessStatusCode();
                
                // Get the stream
                responseStream = await response.Content.ReadAsStreamAsync(context.CancellationToken);
                
                // Process the stream - we pass control to the caller's function
                await streamProcessor(responseStream);
            }
            finally
            {
                // Clean up resources in reverse order
                responseStream?.Dispose();
                response?.Dispose();
            }
        }
        finally
        {
            // Always dispose the request
            request.Dispose();
        }
    }

    /// <inheritdoc/>
    public ValueTask<IList<T>> GetJsonStreamAsync<T>(string url, string? clientName = null, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            ClientName = clientName,
            CancellationToken = cancellationToken
        };
        
        return GetJsonStreamAsync<T>(url, context);
    }

    /// <inheritdoc/>
    public async ValueTask<IList<T>> GetJsonStreamAsync<T>(string url, RequestContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        var result = new List<T>();
        
        await GetStreamAsync(url, async stream => 
        {
            var items = await JsonSerializer.DeserializeAsync<List<T>>(stream, _jsonOptions, context.CancellationToken);
            if (items != null)
            {
                result.AddRange(items);
            }
        }, context);
        
        return result;
    }

    #endregion

    #region Batch Methods

    /// <inheritdoc/>
    public ValueTask<BatchResults> ExecuteBatchAsync(RequestBatch batch, CancellationToken cancellationToken = default)
    {
        var context = new RequestContext
        {
            CancellationToken = cancellationToken
        };
        
        return ExecuteBatchAsync(batch, context);
    }

    /// <inheritdoc/>
    public async ValueTask<BatchResults> ExecuteBatchAsync(RequestBatch batch, RequestContext context)
    {
        if (batch == null) throw new ArgumentNullException(nameof(batch));
        if (context == null) throw new ArgumentNullException(nameof(context));
        
        var results = new BatchResults();
        var tasks = new List<Task>();
        
        foreach (var (request, index) in batch.Requests.Select((req, i) => (req, i)))
        {
            tasks.Add(ExecuteBatchItemAsync(request, index, results, context));
        }
        
        await Task.WhenAll(tasks);
        return results;
    }

    private async Task ExecuteBatchItemAsync(BatchRequestItem request, int index, BatchResults results, RequestContext? context)
    {
        var requestContext = request.Context ?? context ?? new RequestContext();
        
        try
        {
            // The approach differs based on the HTTP method and whether there's data
            if (request.Method == HttpMethod.Get)
            {
                var methodInfo = typeof(MicroHttp).GetMethod(nameof(GetAsync), 
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(string), typeof(RequestContext) },
                    null);
                
                if (methodInfo == null)
                    throw new InvalidOperationException($"Method {nameof(GetAsync)} with parameters (string, RequestContext) not found");
                
                var genericMethod = methodInfo.MakeGenericMethod(request.ResponseType);
                var task = (Task)genericMethod.Invoke(this, new object[] { request.Url, requestContext })!;
                await task;
                
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    var result = resultProperty.GetValue(task);
                    if (result != null)
                    {
                        results.SetResult(index, result);
                    }
                }
            }
            else if (request.Method == HttpMethod.Post && request.Data != null)
            {
                var methodInfo = typeof(MicroHttp).GetMethod(nameof(PostAsync), 
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(string), typeof(object), typeof(RequestContext) },
                    null);
                
                if (methodInfo == null)
                    throw new InvalidOperationException($"Method {nameof(PostAsync)} with parameters (string, object, RequestContext) not found");
                
                var genericMethod = methodInfo.MakeGenericMethod(request.ResponseType);
                var task = (Task)genericMethod.Invoke(this, new object[] { request.Url, request.Data, requestContext })!;
                await task;
                
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    var result = resultProperty.GetValue(task);
                    if (result != null)
                    {
                        results.SetResult(index, result);
                    }
                }
            }
            else if (request.Method == HttpMethod.Put && request.Data != null)
            {
                var methodInfo = typeof(MicroHttp).GetMethod(nameof(PutAsync), 
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(string), typeof(object), typeof(RequestContext) },
                    null);
                
                if (methodInfo == null)
                    throw new InvalidOperationException($"Method {nameof(PutAsync)} with parameters (string, object, RequestContext) not found");
                
                var genericMethod = methodInfo.MakeGenericMethod(request.ResponseType);
                var task = (Task)genericMethod.Invoke(this, new object[] { request.Url, request.Data, requestContext })!;
                await task;
                
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    var result = resultProperty.GetValue(task);
                    if (result != null)
                    {
                        results.SetResult(index, result);
                    }
                }
            }
            else if (request.Method == HttpMethod.Patch && request.Data != null)
            {
                var methodInfo = typeof(MicroHttp).GetMethod(nameof(PatchAsync), 
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(string), typeof(object), typeof(RequestContext) },
                    null);
                
                if (methodInfo == null)
                    throw new InvalidOperationException($"Method {nameof(PatchAsync)} with parameters (string, object, RequestContext) not found");
                
                var genericMethod = methodInfo.MakeGenericMethod(request.ResponseType);
                var task = (Task)genericMethod.Invoke(this, new object[] { request.Url, request.Data, requestContext })!;
                await task;
                
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    var result = resultProperty.GetValue(task);
                    if (result != null)
                    {
                        results.SetResult(index, result);
                    }
                }
            }
            else if (request.Method == HttpMethod.Delete)
            {
                var methodInfo = typeof(MicroHttp).GetMethod(nameof(DeleteAsync), 
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(string), typeof(RequestContext) },
                    null);
                
                if (methodInfo == null)
                    throw new InvalidOperationException($"Method {nameof(DeleteAsync)} with parameters (string, RequestContext) not found");
                
                var genericMethod = methodInfo.MakeGenericMethod(request.ResponseType);
                var task = (Task)genericMethod.Invoke(this, new object[] { request.Url, requestContext })!;
                await task;
                
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty != null)
                {
                    var result = resultProperty.GetValue(task);
                    if (result != null)
                    {
                        results.SetResult(index, result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Unwrap reflection exceptions to get the actual exception
            if (ex is TargetInvocationException tie && tie.InnerException != null)
            {
                throw tie.InnerException;
            }
            throw;
        }
    }

    #endregion

    #region Core Methods

    private async ValueTask<T> SendAsync<T>(HttpRequestMessage request, RequestContext context)
    {
        AddHeaders(request, context.Headers);
        var client = _clientFactory.CreateClient(context.ClientName ?? string.Empty);
        HttpResponseMessage? response = null;
        string? content = null;

        try
        {
            // Process request interceptors if any
            if (context.RequestInterceptors != null && context.RequestInterceptors.Count > 0)
            {
                foreach (var interceptor in context.RequestInterceptors)
                {
                    request = await interceptor.ProcessRequestAsync(request);
                }
            }
            
            // Send the request
            response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.CancellationToken);
            
            // Process response interceptors if any
            if (context.ResponseInterceptors != null && context.ResponseInterceptors.Count > 0)
            {
                foreach (var interceptor in context.ResponseInterceptors)
                {
                    response = await interceptor.ProcessResponseAsync(response);
                }
            }
            
            response.EnsureSuccessStatusCode();
            
            // For string responses, optimize by returning directly
            if (typeof(T) == typeof(string))
            {
                content = await response.Content.ReadAsStringAsync(context.CancellationToken);
                return (T)(object)content;
            }
            
            // Only read content once for JSON deserialization
            content = await response.Content.ReadAsStringAsync(context.CancellationToken);
            var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);
            
            // Check for null result only when needed
            if (result is null)
            {
                throw new JsonException("Failed to deserialize empty or invalid JSON response");
            }
            
            return result;
        }
        catch (Exception ex) when (ex is HttpRequestException or OperationCanceledException or JsonException)
        {
            // We only catch specific exceptions we want to handle specially
            // This avoids the overhead of catching ALL exceptions
            if (ex is HttpRequestException httpEx)
            {
                throw new HttpRequestException($"HTTP request failed: {httpEx.Message}", httpEx, httpEx.StatusCode);
            }
            else if (ex is JsonException jsonEx && content is not null)
            {
                throw new JsonException($"Failed to deserialize response: {content}", jsonEx);
            }
            throw; // For other cases, just rethrow without adding overhead
        }
        finally
        {
            response?.Dispose();
        }
    }

    private async ValueTask SendVoidAsync(HttpRequestMessage request, RequestContext context)
    {
        AddHeaders(request, context.Headers);
        var client = _clientFactory.CreateClient(context.ClientName ?? string.Empty);
        HttpResponseMessage? response = null;

        try
        {
            // Process request interceptors if any
            if (context.RequestInterceptors != null && context.RequestInterceptors.Count > 0)
            {
                foreach (var interceptor in context.RequestInterceptors)
                {
                    request = await interceptor.ProcessRequestAsync(request);
                }
            }
            
            // Send the request
            response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.CancellationToken);
            
            // Process response interceptors if any
            if (context.ResponseInterceptors != null && context.ResponseInterceptors.Count > 0)
            {
                foreach (var interceptor in context.ResponseInterceptors)
                {
                    response = await interceptor.ProcessResponseAsync(response);
                }
            }
            
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException($"HTTP request failed: {ex.Message}", ex, ex.StatusCode);
        }
        finally
        {
            response?.Dispose();
        }
    }

    private HttpRequestMessage CreateJsonRequest(string url, HttpMethod method, [DisallowNull] object data)
    {
        var request = new HttpRequestMessage(method, url);
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        return request;
    }

    private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
    {
        if (headers is null) return;
        
        foreach (var (key, value) in headers)
        {
            if (key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase) && request.Content != null)
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(value);
            }
            else
            {
                request.Headers.TryAddWithoutValidation(key, value);
            }
        }
    }

    #endregion
}
