using Soenneker.Blazor.MediaQuery.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.MediaQuery.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class MediaQueryInteropTests : HostedUnitTest
{
    private readonly IMediaQueryInterop _util;

    public MediaQueryInteropTests(Host host) : base(host)
    {
        _util = Resolve<IMediaQueryInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
