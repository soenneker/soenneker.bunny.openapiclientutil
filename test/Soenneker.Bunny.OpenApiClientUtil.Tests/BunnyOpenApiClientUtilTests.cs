using Soenneker.Bunny.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Bunny.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class BunnyOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IBunnyOpenApiClientUtil _openapiclientutil;

    public BunnyOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IBunnyOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
