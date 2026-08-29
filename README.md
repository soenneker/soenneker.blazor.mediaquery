[![](https://img.shields.io/nuget/v/soenneker.blazor.mediaquery.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.mediaquery/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.mediaquery/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.mediaquery/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.mediaquery.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.mediaquery/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.mediaquery/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.mediaquery/actions/workflows/codeql.yml)

# Soenneker.Blazor.MediaQuery

Defines the media query contract.

## Install

```bash
dotnet add package Soenneker.Blazor.MediaQuery
```

## Quick start

```csharp
using Soenneker.Blazor.MediaQuery.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddMediaQueryInteropAsScoped();
```

Adds `IMediaQuery` as a scoped service.

## What you get

- `IMediaQuery` — Defines the media query contract.
- `IMediaQueryInterop` — A Blazor interop library for media queries for viewport size logic.
- `MediaQueryInteropRegistrar` — A Blazor interop library for media queries for viewport size logic.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IMediaQuery.IsMediaQueryMatched(query, cancellationToken)` | Asynchronously checks if the specified media query matches the current viewport. | true if asynchronously checks if the specified media query matches the current viewport; otherwise, false. |
| `IMediaQueryInterop.Initialize(cancellationToken)` | Initializes the media query so it is ready for use. | A task that completes when the media query is ready for use. |
| `IMediaQueryInterop.Create(dotnetObj, elementId, query, cancellationToken)` | Creates a media query instance from the supplied inputs. | A task that completes when the create operation is complete. |
| `IMediaQueryInterop.IsMediaQueryMatched(query, cancellationToken)` | Determines whether the media query media Query Matched. | true if the media query media Query Matched; otherwise, false. |
| `MediaQueryInteropRegistrar.AddMediaQueryInteropAsScoped(services)` | Adds `IMediaQuery` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Dispose instances you own when their scope ends so held resources can be released.
