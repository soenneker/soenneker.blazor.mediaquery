using Soenneker.Blazor.MediaQuery.Abstract;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;
using System.Threading;
using Soenneker.Extensions.ValueTask;
using System;

namespace Soenneker.Blazor.MediaQuery;

/// <inheritdoc cref="IMediaQueryInterop"/>
public class MediaQueryInterop : IMediaQueryInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncSingleton _scriptInitializer;

    public MediaQueryInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, _) =>
        {
            await _resourceLoader.ImportModuleAndWaitUntilAvailable("Soenneker.Blazor.MediaQuery/mediaqueryinterop.js", "MediaQueryInterop", 100, token).NoSync();
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _scriptInitializer.Init(cancellationToken);
    }

    public async ValueTask Create(DotNetObjectReference<MediaQuery> dotnetObj, string elementId, string query, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken).NoSync();

        await _jsRuntime.InvokeVoidAsync("MediaQueryInterop.addMediaQueryListener", cancellationToken, dotnetObj, elementId, query).NoSync();
    }

    public async ValueTask CreateObserver(string elementId, CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync("MediaQueryInterop.createObserver", cancellationToken, elementId).NoSync();
    }

    public async ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken).NoSync();

        return await _jsRuntime.InvokeAsync<bool>("MediaQueryInterop.isMediaQueryMatched", cancellationToken, query).NoSync();
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        await _resourceLoader.DisposeModule("Soenneker.Blazor.MediaQuery/mediaqueryinterop.js").NoSync();

        await _scriptInitializer.DisposeAsync().NoSync();
    }
}