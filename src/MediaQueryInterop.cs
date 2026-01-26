using Soenneker.Blazor.MediaQuery.Abstract;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Asyncs.Initializers;
using System.Threading;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Blazor.MediaQuery;

/// <inheritdoc cref="IMediaQueryInterop"/>
public sealed class MediaQueryInterop : IMediaQueryInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncInitializer _scriptInitializer;

    private const string _modulePath = "Soenneker.Blazor.MediaQuery/mediaqueryinterop.js";
    private const string _moduleName = "MediaQueryInterop";

    private readonly CancellationScope _cancellationScope = new();

    public MediaQueryInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _resourceLoader = resourceLoader;
        _scriptInitializer = new AsyncInitializer(InitializeScript);
    }

    private ValueTask InitializeScript(CancellationToken token)
    {
        return _resourceLoader.ImportModuleAndWaitUntilAvailable(_modulePath, _moduleName, 100, token);
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            return _scriptInitializer.Init(linked);
    }

    public async ValueTask Create(DotNetObjectReference<MediaQuery> dotnetObj, string elementId, string query, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _scriptInitializer.Init(linked);
            await _jsRuntime.InvokeVoidAsync("MediaQueryInterop.addMediaQueryListener", linked, dotnetObj, elementId, query);
        }
    }

    public ValueTask CreateObserver(string elementId, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            return _jsRuntime.InvokeVoidAsync("MediaQueryInterop.createObserver", linked, elementId);
    }

    public async ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _scriptInitializer.Init(linked);
            return await _jsRuntime.InvokeAsync<bool>("MediaQueryInterop.isMediaQueryMatched", linked, query);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_moduleName);
        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }

    public void Dispose()
    {
        _resourceLoader.DisposeModule(_moduleName);
        _scriptInitializer.Dispose();
        _cancellationScope.DisposeAsync().GetAwaiter().GetResult();
    }
}
