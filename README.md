# MicroHttp
 
This is a C# class named "MicroHttp" that implements an interface called "IMicroHttp". The class provides methods for making HTTP requests using HttpClient, which is created by an instance of IHttpClientFactory.

The constructor of the class accepts an instance of IHttpClientFactory. This factory is used to create HttpClient instances with custom configurations, which are stored as named clients. The named client can be specified as a parameter to the HTTP request methods.

The class provides methods for making GET, POST, PUT, PATCH and DELETE requests. All of these methods accept a URL as a parameter, along with optional parameters for the HTTP headers, cancellation token, and client name.

The POST, PUT, and PATCH request methods also accept a data object, which is serialized to JSON and sent as the HTTP request body.

The SendAsync method is a private method that takes an HTTP request, headers, and a client name, and then sends the request using HttpClient. It also ensures that the response is a success, and deserializes the response body to the generic type specified by the calling method.

The AddHeaders method is a private method that adds HTTP headers to the request if any are provided.

Overall, this class provides a simple and flexible way to make HTTP requests with HttpClient in a .NET application.

# Using MicroHttp in ASP.NET Core
Overview

MicroHttp is a simple and lightweight HTTP client library that provides an easy-to-use interface for making HTTP requests. This document describes how to use MicroHttp in an ASP.NET Core application.
Installation

To use MicroHttp in an ASP.NET Core application, first install the MicroHttp package using the NuGet Package Manager or the command line:

### Installation
```
dotnet add package MicroHttp
```

# Configuration

To use MicroHttp in your ASP.NET Core application, you need to register it with the dependency injection container. To do this, add the following code to your **Program.cs** file:

```csharp
using MicroHttp;

builder.Services.AddMicroHttp();
//custom client
builder.Services.AddHttpClient("microservice-a", c =>
{
    c.BaseAddress = new Uri("http://localhost/");
});
```

The **AddMicroHttp** extension method adds the IMicroHttp interface to the dependency injection container, while the AddHttpClient method configures a named HTTP client with a base address of **http://localhost/**.

# Usage

To use MicroHttp in your ASP.NET Core application, inject an instance of IMicroHttp into your controller or service, and use the **GetAsync**, **PostAsync**, **PutAsync**, **PatchAsync**, and **DeleteAsync** methods to make HTTP requests:

```csharp
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroHttp;

public class HomeController : Controller
{
    private readonly IMicroHttp _http;

    public HomeController(IMicroHttp http)
    {
        _http = http;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _http.GetAsync<string>("test");
        return Content(response);
    }
}
```

In this example, the **Index** action method injects an instance of **IMicroHttp** and uses the **GetAsync** method to make an HTTP GET request to the **http://localhost/test** URL. The response content is returned as a **ContentResult**.

You can also specify a custom HTTP client name when making a request by passing a value to the clientName parameter:

```csharp
var response = await _http.GetAsync<string>("test", "custom-client");
```

You can also pass custom headers to a request by passing a **Dictionary<string, string>** of header name-value pairs to the headers parameter:

```csharp
var headers = new Dictionary<string, string>();
headers.Add("Authorization", "Bearer token");
var response = await _http.GetAsync<string>("test", headers: headers);
```

You can cancel a request by passing a **CancellationToken** to the **cancellationToken** parameter:

```csharp
var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(10));
var response = await _http.GetAsync<string>("test", cancellationToken: cts.Token);
```

# Conclusion

MicroHttp is a lightweight and easy-to-use HTTP client library that is ideal for simple HTTP requests in an ASP.NET Core application. By following the steps in this document, you can easily configure and use MicroHttp in your ASP.NET Core application.

