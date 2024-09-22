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

    private readonly AsyncSingleton<object> _scriptInitializer;

    public MediaQueryInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton<object>(async (token, _) =>
        {
            await _resourceLoader.ImportModuleAndWaitUntilAvailable("Soenneker.Blazor.MediaQuery/mediaqueryinterop.js", "MediaQueryInterop", 100, token);
            return new object();
        });
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        _ = await _scriptInitializer.Get(cancellationToken).NoSync();
    }

    public async ValueTask Create(DotNetObjectReference<MediaQuery> dotnetObj, string elementId, string query, CancellationToken cancellationToken = default)
    {
        _ = await _scriptInitializer.Get(cancellationToken).NoSync();

        await _jsRuntime.InvokeVoidAsync("MediaQueryInterop.addMediaQueryListener", cancellationToken, dotnetObj, elementId, query);
    }

    public async ValueTask CreateObserver(string elementId, CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync("MediaQueryInterop.createObserver", cancellationToken, elementId);
    }

    public async ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default)
    {
        _ = await _scriptInitializer.Get(cancellationToken).NoSync();

        return await _jsRuntime.InvokeAsync<bool>("MediaQueryInterop.isMediaQueryMatched", cancellationToken, query);
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        await _resourceLoader.DisposeModule("Soenneker.Blazor.MediaQuery/mediaqueryinterop.js").NoSync();

        await _scriptInitializer.DisposeAsync().NoSync();
    }
}