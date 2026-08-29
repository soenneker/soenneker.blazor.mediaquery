using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.MediaQuery.Abstract;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Blazor.MediaQuery.Registrars;

/// <summary>
/// A Blazor interop library for media queries for viewport size logic
/// </summary>
public static class MediaQueryInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IMediaQueryInterop"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddMediaQueryInteropAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IMediaQueryInterop, MediaQueryInterop>();

        return services;
    }
}
