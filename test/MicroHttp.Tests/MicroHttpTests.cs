using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using MicroHttp.Interfaces;
using MicroHttp.Models;
using MicroHttp.Interceptors;
using System.Net.Http.Headers;
using System.Reflection;

namespace MicroHttp.Tests;

public class MicroHttpTests
{
    private readonly Mock<IHttpClientFactory> _mockFactory;
    private readonly IMicroHttp _microHttp;
    private readonly JsonSerializerOptions _jsonOptions;

    public MicroHttpTests()
    {
        _mockFactory = new Mock<IHttpClientFactory>();
        _microHttp = new MicroHttp(_mockFactory.Object);
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetAsync_ShouldReturnContent()
    {
        // Arrange
        var expectedResponse = new { Message = "Hello World" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act
        var result = await _microHttp.GetAsync<object>("http://localhost/api/test");

        // Assert
        Assert.NotNull(result);
        var resultContent = JsonSerializer.Serialize(result, _jsonOptions);
        Assert.Equal(responseContent, resultContent);
    }

    [Fact]
    public async Task GetAsync_WithRequestContext_ShouldUseClientNameAndHeaders()
    {
        // Arrange
        var expectedResponse = new { Message = "Hello World" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient("custom-client"))
            .Returns(httpClient);

        var context = new RequestContext
        {
            ClientName = "custom-client",
            Headers = new Dictionary<string, string>
            {
                ["Accept-Language"] = "en-US",
                ["Authorization"] = "Bearer token123"
            }
        };

        // Act
        var result = await _microHttp.GetAsync<object>("http://localhost/api/test", context);

        // Assert
        Assert.NotNull(result);
        Assert.True(handler.RequestWasSent);
        Assert.Equal(2, handler.LastRequest?.Headers.Count());
        Assert.True(handler.LastRequest?.Headers.Contains("Accept-Language"));
        Assert.True(handler.LastRequest?.Headers.Contains("Authorization"));
    }

    [Fact]
    public async Task PostAsync_ShouldReturnContent()
    {
        // Arrange
        var expectedResponse = new { Message = "Hello World" };
        var requestContent = new { Name = "Test" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act
        var result = await _microHttp.PostAsync<object>("http://localhost/api/test", requestContent);

        // Assert
        Assert.NotNull(result);
        Assert.True(handler.RequestWasSent);
        Assert.Equal(HttpMethod.Post, handler.LastRequest?.Method);
        var resultContent = JsonSerializer.Serialize(result, _jsonOptions);
        Assert.Equal(responseContent, resultContent);
    }

    [Fact]
    public async Task PutAsync_ShouldReturnContent()
    {
        // Arrange
        var expectedResponse = new { Message = "Hello World" };
        var requestContent = new { Name = "Test" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act
        var result = await _microHttp.PutAsync<object>("http://localhost/api/test", requestContent);

        // Assert
        Assert.NotNull(result);
        Assert.True(handler.RequestWasSent);
        Assert.Equal(HttpMethod.Put, handler.LastRequest?.Method);
        var resultContent = JsonSerializer.Serialize(result, _jsonOptions);
        Assert.Equal(responseContent, resultContent);
    }

    [Fact]
    public async Task PatchAsync_ShouldReturnContent()
    {
        // Arrange
        var expectedResponse = new { Message = "Hello World" };
        var requestContent = new { Name = "Test" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act
        var result = await _microHttp.PatchAsync<object>("http://localhost/api/test", requestContent);

        // Assert
        Assert.NotNull(result);
        Assert.True(handler.RequestWasSent);
        Assert.Equal(HttpMethod.Patch, handler.LastRequest?.Method);
        var resultContent = JsonSerializer.Serialize(result, _jsonOptions);
        Assert.Equal(responseContent, resultContent);
    }

    [Fact]
    public async Task DeleteAsync_ShouldExecuteSuccessfully()
    {
        // Arrange
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.NoContent));
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act & Assert (no exception means success)
        await _microHttp.DeleteAsync("http://test.com/api/items/1");
        
        // Assert
        Assert.True(handler.RequestWasSent);
        Assert.Equal(HttpMethod.Delete, handler.LastRequest?.Method);
    }

    [Fact]
    public async Task GetStringAsync_ShouldReturnRawContent()
    {
        // Arrange
        var rawContent = "Hello World";
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(rawContent, Encoding.UTF8, "text/plain")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act
        var result = await _microHttp.GetAsync<string>("http://localhost/api/test");

        // Assert
        Assert.Equal(rawContent, result);
    }

    [Fact]
    public async Task PostFileAsync_ShouldUploadFileSuccessfully()
    {
        // Arrange
        var expectedResponse = new { FileId = "123", Status = "Uploaded" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Create a file upload request
        using var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("test file content"));
        var fileRequest = new FileUploadRequest
        {
            File = new FileContent("test.txt", fileStream) { ContentType = "text/plain" },
            FormFields = new Dictionary<string, string>
            {
                ["description"] = "Test file upload"
            }
        };

        // Act
        var result = await _microHttp.PostFileAsync<object>("http://localhost/api/uploads", fileRequest);

        // Assert
        Assert.NotNull(result);
        Assert.True(handler.RequestWasSent);
        Assert.Equal(HttpMethod.Post, handler.LastRequest?.Method);
        var resultContent = JsonSerializer.Serialize(result, _jsonOptions);
        Assert.Equal(responseContent, resultContent);
    }

    [Fact]
    public async Task GetStreamAsync_ShouldProcessStreamSuccessfully()
    {
        // Arrange
        var streamContent = "Test stream content";
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(streamContent, Encoding.UTF8, "text/plain")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        var processedContent = string.Empty;

        // Act
        await _microHttp.GetStreamAsync("http://localhost/api/stream", async stream =>
        {
            using var reader = new StreamReader(stream);
            processedContent = await reader.ReadToEndAsync();
        });

        // Assert
        Assert.Equal(streamContent, processedContent);
        Assert.True(handler.RequestWasSent);
        Assert.Equal(HttpMethod.Get, handler.LastRequest?.Method);
    }

    [Fact]
    public async Task GetJsonStreamAsync_ShouldDeserializeJsonStreamSuccessfully()
    {
        // Arrange
        var items = new[] { new { Id = 1, Name = "Item 1" }, new { Id = 2, Name = "Item 2" } };
        var responseContent = JsonSerializer.Serialize(items, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Act
        var result = await _microHttp.GetJsonStreamAsync<TestItem>("http://localhost/api/items");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Item 1", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Item 2", result[1].Name);
    }

    [Fact]
    public async Task WithRequestInterceptor_ShouldModifyRequest()
    {
        // Arrange
        var expectedResponse = new { Message = "Hello World" };
        var responseContent = JsonSerializer.Serialize(expectedResponse, _jsonOptions);
        var handler = new MockHttpMessageHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
        });
        var httpClient = new HttpClient(handler);

        _mockFactory
            .Setup(cf => cf.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Create a request interceptor that adds a header
        var interceptor = new TestRequestInterceptor("X-Custom-Header", "TestValue");
        var context = new RequestContext
        {
            RequestInterceptors = new List<IRequestInterceptor> { interceptor }
        };

        // Act
        var result = await _microHttp.GetAsync<object>("http://localhost/api/test", context);

        // Assert
        Assert.NotNull(result);
        Assert.True(handler.RequestWasSent);
        Assert.True(handler.LastRequest?.Headers.Contains("X-Custom-Header"));
        Assert.Equal("TestValue", handler.LastRequest?.Headers.GetValues("X-Custom-Header").First());
    }

    // Helper methods to call the generic methods
    private async Task<object> GetAsync(Type responseType, string url, RequestContext? context)
    {
        // Use a dynamic approach or create specific helper methods for each type
        // This example is simplified
        MethodInfo method = typeof(MicroHttp).GetMethod(nameof(GetAsync), new[] { typeof(string), typeof(RequestContext) });
        MethodInfo genericMethod = method.MakeGenericMethod(responseType);
        return await (dynamic)genericMethod.Invoke(this, new object[] { url, context });
    }

    private async Task<object> PostAsync(Type responseType, string url, object data, RequestContext? context)
    {
        MethodInfo method = typeof(MicroHttp).GetMethod(nameof(PostAsync), new[] { typeof(string), typeof(object), typeof(RequestContext) });
        MethodInfo genericMethod = method.MakeGenericMethod(responseType);
        return await (dynamic)genericMethod.Invoke(this, new object[] { url, data, context });
    }

    public class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>
    /// A test request interceptor that adds a custom header
    /// </summary>
    private class TestRequestInterceptor : IRequestInterceptor
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public TestRequestInterceptor(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public Task<HttpRequestMessage> ProcessRequestAsync(HttpRequestMessage request)
        {
            request.Headers.Add(_headerName, _headerValue);
            return Task.FromResult(request);
        }
    }

    /// <summary>
    /// A custom mock HttpMessageHandler to help with testing
    /// </summary>
    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public bool RequestWasSent { get; private set; }
        public HttpRequestMessage? LastRequest { get; private set; }

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestWasSent = true;
            LastRequest = request;
            return Task.FromResult(_response);
        }
    }

    /// <summary>
    /// A handler that can respond differently to multiple requests
    /// </summary>
    private class MockMultiHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _responseFactory;

        public MockMultiHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
        {
            _responseFactory = responseFactory;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_responseFactory(request));
        }
    }

    /// <summary>
    /// Custom handler for testing that counts requests and uses a factory for responses
    /// </summary>
    private class TestDelegatingHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _responseFactory;
        public int RequestCount { get; private set; }
        
        public TestDelegatingHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
        {
            _responseFactory = responseFactory;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestCount++;
            return Task.FromResult(_responseFactory(request));
        }
    }

    /// <summary>
    /// A simplified handler for batch testing that returns predefined responses based on URL
    /// </summary>
    private class SimpleBatchHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, object> _responses = new();
        
        public void AddResponse(string url, object response)
        {
            _responses[url] = response;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get the base URL without query parameters
            var url = request.RequestUri?.GetLeftPart(UriPartial.Path);
            
            if (url != null && _responses.TryGetValue(url, out var response))
            {
                var content = JsonSerializer.Serialize(response);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                });
            }
            
            // Default fallback response
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            });
        }
    }

    /// <summary>
    /// Test implementation of BatchResults for use in unit tests
    /// </summary>
    private class TestBatchResults
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
        public void SetResult(int index, object result)
        {
            _results[index] = result;
        }
        
        /// <summary>
        /// Gets the number of results.
        /// </summary>
        public int Count => _results.Count;
    }
}