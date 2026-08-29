using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.JSInterop;

namespace Soenneker.Blazor.MediaQuery.Abstract;

/// <summary>
/// A Blazor interop library for media queries for viewport size logic
/// </summary>
public interface IMediaQueryInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the media query so it is ready for use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the media query is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a media query instance from the supplied inputs.
    /// </summary>
    /// <param name="dotnetObj">Dotnet Obj for the create operation.</param>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="query">CSS media-query expression to evaluate against the current viewport.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the create operation is complete.</returns>
    ValueTask Create(DotNetObjectReference<MediaQuery> dotnetObj, string elementId, string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates observer.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the observer creation is complete.</returns>
    ValueTask CreateObserver(string elementId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the media-query listener and its DOM observer.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to tear down.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when teardown is complete.</returns>
    ValueTask Destroy(string elementId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether the media query media Query Matched.
    /// </summary>
    /// <param name="query">CSS media-query expression to evaluate against the current viewport.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if the media query media Query Matched; otherwise, false.</returns>
    ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default);
}
