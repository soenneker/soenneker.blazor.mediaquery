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
    /// <param name="query">CSS media-query expression to evaluate against the current viewport.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if asynchronously checks if the specified media query matches the current viewport; otherwise, false.</returns>
    ValueTask<bool> IsMediaQueryMatched(string query, CancellationToken cancellationToken = default);
}
