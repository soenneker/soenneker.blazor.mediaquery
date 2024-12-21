using Soenneker.Blazor.MediaQuery.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.MediaQuery.Tests;

[Collection("Collection")]
public class MediaQueryInteropTests : FixturedUnitTest
{
    private readonly IMediaQueryInterop _util;

    public MediaQueryInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IMediaQueryInterop>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
