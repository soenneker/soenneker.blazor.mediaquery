using System.Threading;
using System.Threading.Tasks;
using Soenneker.Lepton.Suite.Abstract;

namespace Soenneker.Blazor.MediaQuery.Abstract;

/// <summary>
/// Defines the media query contract.
/// </summary>
public interface IMediaQuery : ILeptonCancellableIdentifiableContentElement
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