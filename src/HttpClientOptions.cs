using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;

namespace Soenneker.Dtos.HttpClientOptions;

/// <summary>
/// A DTO type for common HttpClient options functionality
/// </summary>
public sealed class HttpClientOptions
{
    /// <summary>
    /// Gets or sets the maximum lifetime of a connection in the connection pool before it is discarded.
    /// A value of <see langword="null"/> indicates that the connection will not have a limited lifetime.
    /// Default when null: 600 seconds (10 minutes). Framework default: infinite (no limit).
    /// </summary>
    public TimeSpan? PooledConnectionLifetime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="HttpClient"/> should use a cookie container to store and manage cookies.
    /// A value of <see langword="null"/> indicates that the default behavior of the client will be used.
    /// Default when null: <see langword="false"/>. Framework default: <see langword="false"/>.
    /// </summary>
    public bool? UseCookieContainer { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of concurrent connections allowed per server.
    /// A value of <see langword="null"/> indicates that the default value will be used.
    /// Default when null: 40. Framework default: <see cref="int.MaxValue"/>.
    /// </summary>
    public int? MaxConnectionsPerServer { get; set; }

    /// <summary>
    /// Gets or sets the time to wait before the request times out.
    /// This is the overall <see cref="HttpClient.Timeout"/> (request timeout), not the connect timeout.
    /// Default when null: 100 seconds. Framework default: 100 seconds.
    /// </summary>
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Gets or sets the time to wait when establishing a TCP connection.
    /// This maps to <see cref="SocketsHttpHandler.ConnectTimeout"/>.
    /// A value of <see langword="null"/> indicates that the framework default will be used (typically Infinite).
    /// </summary>
    public TimeSpan? ConnectTimeout { get; set; }

    /// <summary>
    /// Gets or sets a collection of default headers to be included with each request.
    /// A value of <see langword="null"/> indicates that no default headers will be added.
    /// </summary>
    public Dictionary<string, string>? DefaultRequestHeaders { get; set; }

    /// <summary>
    /// Gets or sets a function to modify the <see cref="HttpClient"/> after it has been created.
    /// This function is only executed during the first retrieval of the client.
    /// </summary>
    public Func<HttpClient, ValueTask>? ModifyClient { get; set; }
    
    /// <summary>
    /// Gets or sets the base address of the <see cref="HttpClient"/> as a <see cref="Uri"/>.
    /// </summary>
    public Uri? BaseAddress { get; set; }

    /// <summary>
    /// Gets or sets a custom <see cref="HttpClientHandler"/> to use instead of creating a new <see cref="SocketsHttpHandler"/>.
    /// When this is set, all other handler-specific options are ignored and this handler is used directly.
    /// A value of <see langword="null"/> indicates that a <see cref="SocketsHttpHandler"/> will be created with the configured options.
    /// </summary>
    public HttpClientHandler? HttpClientHandler { get; set; }

    /// <summary>
    /// Gets or sets the timeout when draining response content.
    /// A value of <see langword="null"/> indicates that the default timeout will be used.
    /// Default when null: 2 seconds. Framework default: 2 seconds.
    /// </summary>
    public TimeSpan? ResponseDrainTimeout { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the handler should automatically follow redirect responses.
    /// A value of <see langword="null"/> indicates that the default behavior will be used.
    /// Default when null: <see langword="true"/>. Framework default: <see langword="true"/>.
    /// </summary>
    public bool? AllowAutoRedirect { get; set; }

    /// <summary>
    /// Gets or sets the automatic decompression method for response content.
    /// A value of <see langword="null"/> indicates that the default behavior will be used.
    /// Default when null: <see cref="DecompressionMethods.None"/>. Framework default: <see cref="DecompressionMethods.None"/>.
    /// </summary>
    public DecompressionMethods? AutomaticDecompression { get; set; }

    /// <summary>
    /// Gets or sets the delay between keep-alive pings.
    /// A value of <see langword="null"/> indicates that the default delay will be used.
    /// Default when null: Infinite (no keep-alive pings). Framework default: Infinite.
    /// </summary>
    public TimeSpan? KeepAlivePingDelay { get; set; }

    /// <summary>
    /// Gets or sets the timeout for keep-alive pings.
    /// A value of <see langword="null"/> indicates that the default timeout will be used.
    /// Default when null: 20 seconds. Framework default: 20 seconds.
    /// </summary>
    public TimeSpan? KeepAlivePingTimeout { get; set; }

    /// <summary>
    /// Gets or sets the keep-alive ping policy.
    /// A value of <see langword="null"/> indicates that the default policy will be used.
    /// Default when null: <see cref="HttpKeepAlivePingPolicy.Always"/>. Framework default: <see cref="HttpKeepAlivePingPolicy.Always"/>.
    /// </summary>
    public HttpKeepAlivePingPolicy? KeepAlivePingPolicy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the handler should use a proxy.
    /// A value of <see langword="null"/> indicates that the default behavior will be used.
    /// Default when null: <see langword="true"/>. Framework default: <see langword="true"/>.
    /// </summary>
    public bool? UseProxy { get; set; }

    /// <summary>
    /// Gets or sets the proxy to use for requests.
    /// A value of <see langword="null"/> indicates that the default proxy will be used.
    /// Default when null: System proxy (from <see cref="HttpClient.DefaultProxy"/>). Framework default: System proxy.
    /// </summary>
    public IWebProxy? Proxy { get; set; }

    /// <summary>
    /// Gets or sets the maximum size for draining response content.
    /// A value of <see langword="null"/> indicates that the default size will be used.
    /// Default when null: 1,048,576 bytes (1 MB). Framework default: 1,048,576 bytes (1 MB).
    /// </summary>
    public int? MaxResponseDrainSize { get; set; }

    /// <summary>
    /// Gets or sets the maximum length of response headers.
    /// A value of <see langword="null"/> indicates that the default length will be used.
    /// Default when null: 65,536 bytes (64 KB). Framework default: 65,536 bytes (64 KB).
    /// </summary>
    public int? MaxResponseHeadersLength { get; set; }

    /// <summary>
    /// Gets or sets the SSL options for secure connections.
    /// A value of <see langword="null"/> indicates that the default SSL options will be used.
    /// Default when null: Framework default SSL/TLS options (no custom configuration).
    /// </summary>
    public SslClientAuthenticationOptions? SslOptions { get; set; }
}