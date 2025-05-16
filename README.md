# MicroHttp

MicroHttp is a high-performance HTTP client library optimized specifically for .NET 9 applications. It provides a clean, efficient interface for making HTTP requests with minimal overhead while leveraging the latest performance features.

# Overview

This C# class named "MicroHttp" implements an interface called "IMicroHttp". The class provides methods for making HTTP requests using HttpClient, which is created by an instance of IHttpClientFactory.

The constructor of the class accepts an instance of IHttpClientFactory. This factory is used to create HttpClient instances with custom configurations, which are stored as named clients. The named client can be specified as a parameter to the HTTP request methods or within a RequestContext.

# Key Features

•	Optimized for .NET 9: Takes full advantage of the latest performance enhancements
•	High Performance: Uses ValueTask for efficient asynchronous operations
•	Modern C# Features: Leverages file-scoped namespaces and improved null handling
•	System.Text.Json Integration: Uses the high-performance native JSON serializer
•	Memory Efficient: Minimizes allocations and properly disposes of resources
•	Improved Error Handling: Better exception information and status code reporting
•	Batch Request Processing: Execute multiple HTTP requests in a single operation
•	Dual API Design: Choose between simple parameters or flexible RequestContext
•	Request/Response Interceptors: Modify requests and responses with custom logic
•	File Upload Support: Simplified API for uploading single or multiple files
•	Streaming Support: Process large responses efficiently with streaming

### Installation

To use MicroHttp in an ASP.NET Core application, first install the MicroHttp package using the NuGet Package Manager or the command line:

```
dotnet add package MicroHttp
```

# Configuration

To use MicroHttp in your ASP.NET Core application, you need to register it with the dependency injection container. To do this, add the following code to your Program.cs file:

```csharp
using MicroHttp.Helper;

// Basic configuration
builder.Services.AddMicroHttp();

// Custom client configuration
builder.Services.AddHttpClient("microservice-a", c =>
{
    c.BaseAddress = new Uri("http://localhost/");
    c.DefaultRequestVersion = new Version(3, 0); // HTTP/3 support
});
```
The **AddMicroHttp** extension method adds the IMicroHttp interface to the dependency injection container, while the AddHttpClient method configures a named HTTP client with a base address of **http://localhost/**

# Basic Usage

MicroHttp offers two ways to make requests:

# Simple Parameter Approach

```csharp
using Microsoft.AspNetCore.Mvc;
using MicroHttp.Interfaces;

public class WeatherController : Controller
{
    private readonly IMicroHttp _http;

    public WeatherController(IMicroHttp http)
    {
        _http = http;
    }

    public async Task<IActionResult> GetForecast(string city)
    {
        // Most basic request
        var forecast = await _http.GetAsync<WeatherForecast>($"api/weather/{city}");
        
        // With client name
        var forecast2 = await _http.GetAsync<WeatherForecast>($"api/weather/{city}", "weather-service");
        
        // With cancellation token
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var forecast3 = await _http.GetAsync<WeatherForecast>($"api/weather/{city}", null, cts.Token);
        
        return View(forecast);
    }
    
    public async Task<IActionResult> CreateReport(ReportRequest request)
    {
        // POST request with data
        var result = await _http.PostAsync<ReportResponse>("api/reports", request);
        
        return RedirectToAction("ViewReport", new { id = result.ReportId });
    }
}
```

# RequestContext Approach

```csharp
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroHttp.Interfaces;
using MicroHttp.Models;

public class OrderController : Controller
{
    private readonly IMicroHttp _http;

    public OrderController(IMicroHttp http)
    {
        _http = http;
    }

    public async Task<IActionResult> GetOrderDetails(int orderId)
    {
        // Advanced request with RequestContext
        var context = new RequestContext
        {
            ClientName = "orders-api",
            Headers = new Dictionary<string, string>
            {
                ["X-Correlation-ID"] = Guid.NewGuid().ToString(),
                ["Authorization"] = "Bearer " + GetUserToken()
            },
            CancellationToken = HttpContext.RequestAborted
        };
        
        var order = await _http.GetAsync<OrderDetails>($"api/orders/{orderId}", context);
        return View(order);
    }
    
    private string GetUserToken() => User.Claims.FirstOrDefault(c => c.Type == "token")?.Value;
}
```

In this example, the Index action method injects an instance of IMicroHttp and uses the GetAsync method to make an HTTP GET request to the http://localhost/test URL. The response content is returned as a ContentResult.

# RequestContext Builder

You can use the builder pattern to create RequestContext objects:

```csharp
var context = RequestContext.CreateBuilder()
    .WithClient("custom-client")
    .WithHeader("Authorization", "Bearer token123")
    .WithHeader("X-Correlation-ID", Guid.NewGuid().ToString())
    .WithCancellation(cts.Token)
    .Build();

var response = await _http.GetAsync<UserProfile>("api/users/me", context);
```

# Caching Policies

Control how responses are cached:

```csharp
// Default 5-minute cache
var context = new RequestContext
{
    CachePolicy = CachePolicy.Default
};

// Custom cache duration with sliding expiration
var context = new RequestContext
{
    CachePolicy = new CachePolicy
    {
        Duration = TimeSpan.FromMinutes(10),
        SlidingExpiration = true,
        CustomCacheKeyParts = new[] { "user-123" }
    }
};

// Force refresh cached data
var context = new RequestContext
{
    CachePolicy = new CachePolicy { ForceRefresh = true }
};

// Disable caching entirely
var context = new RequestContext
{
    CachePolicy = CachePolicy.NoCache
};
```

# File Uploads

Upload single or multiple files with form data:

```csharp
public async Task<IActionResult> UploadDocument(IFormFile file)
{
    using var stream = file.OpenReadStream();
    
    // Create file upload request
    var uploadRequest = new FileUploadRequest
    {
        File = new FileContent(file.FileName, stream)
        {
            ContentType = file.ContentType,
            FieldName = "document"
        },
        FormFields = new Dictionary<string, string>
        {
            ["documentType"] = "invoice",
            ["description"] = "Monthly invoice"
        }
    };
    
    // Upload and get response
    var result = await _http.PostFileAsync<DocumentResponse>("api/documents/upload", uploadRequest);
    
    return RedirectToAction("ViewDocument", new { id = result.DocumentId });
}
```

# Streaming Support

Process large files or data streams efficiently:

```csharp
// Download and process a large file directly
await _http.GetStreamAsync("api/reports/large.csv", async stream => 
{
    using var fileStream = new FileStream("report.csv", FileMode.Create);
    await stream.CopyToAsync(fileStream);
});

// Process a JSON stream of objects
var allItems = await _http.GetJsonStreamAsync<DataItem>("api/data/stream");
```

# Batch Request Processing

Execute multiple requests in a single operation:

```csharp
// Create a batch of requests
var batch = new RequestBatch();

// Add various request types to the batch
var userIndex = batch.Add<UserProfile>("api/users/current");
var productsIndex = batch.Add<List<Product>>("api/products/featured");
var orderIndex = batch.AddPost<OrderResult>("api/orders", new OrderRequest { ProductId = 123, Quantity = 1 });

// Execute all requests in the batch
var results = await _http.ExecuteBatchAsync(batch);

// Access the results by index and type
var user = results.Get<UserProfile>(userIndex);
var products = results.Get<List<Product>>(productsIndex);
var order = results.Get<OrderResult>(orderIndex);

// Check how many results were returned
Console.WriteLine($"Batch returned {results.Count} results");
```

# Request and Response Interceptors

Modify requests or responses with custom logic:

```csharp
// Create a request interceptor
public class AuthInterceptor : IRequestInterceptor
{
    public Task<HttpRequestMessage> ProcessRequestAsync(HttpRequestMessage request)
    {
        request.Headers.Add("Authorization", "Bearer " + GetToken());
        return Task.FromResult(request);
    }
    
    private string GetToken() => "your-auth-token";
}

// Create a response interceptor
public class LoggingInterceptor : IResponseInterceptor
{
    public Task<HttpResponseMessage> ProcessResponseAsync(HttpResponseMessage response)
    {
        Console.WriteLine($"Response status: {response.StatusCode}");
        return Task.FromResult(response);
    }
}

// Use the interceptors
var context = new RequestContext
{
    RequestInterceptors = new List<IRequestInterceptor> { new AuthInterceptor() },
    ResponseInterceptors = new List<IResponseInterceptor> { new LoggingInterceptor() }
};

var response = await _http.GetAsync<DataResponse>("api/data", context);
```

# Performance Optimizations

The .NET 9 version of MicroHttp includes several performance enhancements:
•	Uses ValueTask instead of Task for better performance with short-running operations
•	Leverages System.Text.Json for faster serialization and deserialization
•	Implements targeted exception handling to minimize overhead
•	Uses HttpCompletionOption.ResponseHeadersRead for streaming response handling
•	Special optimizations for string responses
•	Efficient HTTP header handling
•	Proper resource disposal to prevent memory leaks
•	Support for modern HTTP features like HTTP/3

# Error Handling

MicroHttp provides improved error handling with specific exception types:

```csharp
try
{
    var result = await _http.GetAsync<DataResponse>("api/data");
    // Process result
}
catch (HttpRequestException ex)
{
    // Access status code and details
    Console.WriteLine($"HTTP error: {ex.StatusCode} - {ex.Message}");
}
catch (JsonException ex)
{
    // Handle deserialization errors
    Console.WriteLine($"JSON error: {ex.Message}");
}
catch (OperationCanceledException ex)
{
    // Handle timeouts or cancellations
    Console.WriteLine("Request was canceled or timed out");
}
```

# Conclusion
MicroHttp is a lightweight and high-performance HTTP client library that is ideal for modern ASP.NET Core applications built on .NET 9. It provides both simple and advanced APIs for making HTTP requests, with significant performance optimizations that make it efficient for everything from basic API calls to complex integration scenarios. With features like dual API design, batch processing, request contexts, caching policies, and interceptors, it offers a flexible and powerful solution for all your HTTP communication needs.