using Soenneker.Bunny.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Bunny.OpenApiClientUtil.Abstract;

/// <summary>
/// Creates and caches an authenticated <see cref="BunnyOpenApiClient"/>.
/// </summary>
public interface IBunnyOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel initial client creation.</param>
    /// <returns>The cached generated client.</returns>
    ValueTask<BunnyOpenApiClient> Get(CancellationToken cancellationToken = default);
}
