[![](https://img.shields.io/nuget/v/soenneker.bunny.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bunny.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bunny.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.bunny.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.bunny.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bunny.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bunny.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.bunny.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Bunny.OpenApiClientUtil

Creates and caches an authenticated `BunnyOpenApiClient` for dependency-injected applications.

## Installation

```bash
dotnet add package Soenneker.Bunny.OpenApiClientUtil
```

## Configuration

```json
{
  "Bunny": {
    "ApiKey": "your-access-key"
  }
}
```

`Bunny:ApiKey` is required. The defaults are `https://api.bunny.net` and `AccessKey: {token}`. `Bunny:ClientBaseUrl`, `Bunny:AuthHeaderName`, and `Bunny:AuthHeaderValueTemplate` can override them.

The generated client combines products that use different hosts and credentials. Configure one utility per host/credential set; specialized storage, Stream, JWT, and other endpoints may need different settings.

## Registration

```csharp
using Soenneker.Bunny.OpenApiClientUtil.Registrars;

services.AddBunnyOpenApiClientUtilAsScoped();
```

The scoped utility borrows a singleton HTTP-client provider. Ending a scope disposes its generated client state but leaves the singleton provider and `HttpClient` alive. Use `AddBunnyOpenApiClientUtilAsSingleton()` to share the generated client too.

## Usage

```csharp
using Soenneker.Bunny.OpenApiClient;
using Soenneker.Bunny.OpenApiClient.Models;
using Soenneker.Bunny.OpenApiClientUtil.Abstract;

public sealed class PullZoneService
{
    private readonly IBunnyOpenApiClientUtil _clientUtil;

    public PullZoneService(IBunnyOpenApiClientUtil clientUtil) => _clientUtil = clientUtil;

    public async Task<List<PullZoneModel>?> Get(CancellationToken cancellationToken = default)
    {
        BunnyOpenApiClient client = await _clientUtil.Get(cancellationToken);
        return await client.Core.Pullzone.GetAsync(cancellationToken: cancellationToken);
    }
}
```

`Get()` lazily creates one generated client per utility instance. Configuration is captured during creation. Credentials are added only to HTTPS requests and are pinned to the first request host.
