[![](https://img.shields.io/nuget/v/soenneker.dtos.httpclientoptions.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.httpclientoptions/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.httpclientoptions/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.httpclientoptions/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dtos.httpclientoptions.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.httpclientoptions/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.httpclientoptions/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.httpclientoptions/actions/workflows/codeql.yml)

# Soenneker.Dtos.HttpClientOptions

Describes client, transport, and handler-pipeline settings for `Soenneker.Utils.HttpClientCache`.

## Installation

```bash
dotnet add package Soenneker.Dtos.HttpClientOptions
```

## Common configuration

```csharp
using System.Net;
using Soenneker.Dtos.HttpClientOptions;

var options = new HttpClientOptions
{
    BaseAddress = new Uri("https://api.example.com/"),
    Timeout = TimeSpan.FromSeconds(30),
    ConnectTimeout = TimeSpan.FromSeconds(5),
    PooledConnectionLifetime = TimeSpan.FromMinutes(10),
    MaxConnectionsPerServer = 40,
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
    DefaultRequestHeaders = new Dictionary<string, string>
    {
        ["Accept"] = "application/json"
    }
};
```

`Timeout` applies to the overall `HttpClient` request. `ConnectTimeout` applies only while establishing a connection. When consumed by `HttpClientCache`, null values use a 100-second request timeout, a 100-second connect timeout, a 10-minute pooled-connection lifetime, and 40 connections per server.

## Cookies, redirects, and proxies

```csharp
var options = new HttpClientOptions
{
    UseCookieContainer = true,
    AllowAutoRedirect = false,
    UseProxy = true,
    Proxy = new WebProxy("http://proxy.example.com:8080")
};
```

Cookies are disabled unless `UseCookieContainer` is true. Enabling them gives the cached client a dedicated cookie container rather than a shared transport. Supplying `Proxy`, `SslOptions`, or `ModifyPrimaryHandler` also creates a dedicated transport.

Automatic redirects default to the handler behavior (enabled). Disable them when non-`Authorization` default headers contain secrets and redirected hosts are not fully trusted.

## Customize client and handlers

```csharp
var options = new HttpClientOptions
{
    ModifyClient = client =>
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("ExampleClient/1.0");
        return ValueTask.CompletedTask;
    },
    ModifyPrimaryHandler = handler =>
    {
        handler.EnableMultipleHttp2Connections = true;
    },
    DelegatingHandlerFactories =
    [
        static () => new CorrelationHandler(),
        static () => new MetricsHandler()
    ]
};
```

Callbacks and factories run once when the cached client is created. Each delegating-handler factory must return a new handler whose `InnerHandler` is unset. The first factory becomes the outermost handler. The cache owns and disposes the resulting pipeline.

## Remaining transport limits

- `ResponseDrainTimeout` and `MaxResponseDrainSize` control draining content when responses are disposed before their bodies are consumed. Drain size is measured in bytes.
- `MaxResponseHeadersLength` is measured in kilobytes; the framework default is 64 KB.
- `KeepAlivePingDelay`, `KeepAlivePingTimeout`, and `KeepAlivePingPolicy` configure HTTP/2 or HTTP/3 keep-alive pings.
- `SslOptions` replaces TLS client-authentication settings for a dedicated handler. Avoid certificate-validation callbacks that accept untrusted certificates.

This is a runtime options object, not a portable serialization contract: it contains delegates, handlers, proxy objects, and TLS settings. Construct it in code. The cache reads it only during first initialization for a key, so later calls with different options—or mutations after creation—should not be used to reconfigure an existing client.
