using System.Threading;
using System.Threading.Tasks;
using Soenneker.Quark.Components.Core.Cancellable.Abstract;

namespace Soenneker.Blazor.MediaQuery.Abstract;

public interface IMediaQuery : ICoreCancellableElement
{
    /// <summary>
    /// Asynchronously checks if the specified media query matches the current viewport.
    /// </summary>
    /// <param name="query">The media query string to evaluate.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that represents the asynchronous operation, containing a boolean value
    /// indicating whether the media query matches.</returns>
    ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default);
}