using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.MediaQuery.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.MediaQuery.Registrars;

/// <summary>
/// A Blazor interop library for media queries for viewport size logic
/// </summary>
public static class MediaQueryInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IMediaQuery"/> as a scoped service. <para/>
    /// </summary>
    public static void AddMediaQuery(this IServiceCollection services)
    {
        services.AddResourceLoader();
        services.TryAddScoped<IMediaQueryInterop, MediaQueryInterop>();
    }
}