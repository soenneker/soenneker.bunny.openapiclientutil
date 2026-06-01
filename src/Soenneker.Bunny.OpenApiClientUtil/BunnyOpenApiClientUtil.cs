using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Bunny.HttpClients.Abstract;
using Soenneker.Bunny.OpenApiClientUtil.Abstract;
using Soenneker.Bunny.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Bunny.OpenApiClientUtil;

///<inheritdoc cref="IBunnyOpenApiClientUtil"/>
public sealed class BunnyOpenApiClientUtil : IBunnyOpenApiClientUtil
{
    private readonly AsyncSingleton<BunnyOpenApiClient> _client;

    public BunnyOpenApiClientUtil(IBunnyOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<BunnyOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Bunny:ApiKey");
            string authHeaderValueTemplate = configuration["Bunny:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new BunnyOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<BunnyOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
