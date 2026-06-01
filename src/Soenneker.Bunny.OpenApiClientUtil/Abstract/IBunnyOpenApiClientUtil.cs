using Soenneker.Bunny.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Bunny.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IBunnyOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<BunnyOpenApiClient> Get(CancellationToken cancellationToken = default);
}
