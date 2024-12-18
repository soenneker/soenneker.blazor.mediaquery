using Soenneker.Blazor.MediaQuery.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.MediaQuery.Tests;

[Collection("Collection")]
public class MediaQueryInteropTests : FixturedUnitTest
{
    private readonly IMediaQueryInterop _interop;

    public MediaQueryInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _interop = Resolve<IMediaQueryInterop>(true);
    }
}
