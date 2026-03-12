using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.JSInterop;

namespace Soenneker.Blazor.MediaQuery.Abstract;

/// <summary>
/// A Blazor interop library for media queries for viewport size logic
/// </summary>
public interface IMediaQueryInterop : IAsyncDisposable, IDisposable
{
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask Create(DotNetObjectReference<MediaQuery> dotnetObj, string elementId, string query, CancellationToken cancellationToken = default);

    ValueTask CreateObserver(string elementId, CancellationToken cancellationToken = default);

    ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default);
}
