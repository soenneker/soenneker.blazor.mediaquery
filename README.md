[![](https://img.shields.io/nuget/v/soenneker.blazor.mediaquery.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.mediaquery/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.mediaquery/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.mediaquery/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.mediaquery.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.mediaquery/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.mediaquery/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.mediaquery/actions/workflows/codeql.yml)

# Soenneker.Blazor.MediaQuery

A Blazor component that conditionally renders content from a browser CSS media query, plus an interop service for one-time `matchMedia` checks.

## Installation

```bash
dotnet add package Soenneker.Blazor.MediaQuery
```

```csharp
using Soenneker.Blazor.MediaQuery.Registrars;

builder.Services.AddMediaQueryInteropAsScoped();
```

Add the component namespace to `_Imports.razor`:

```razor
@using Soenneker.Blazor.MediaQuery
```

## Conditional content

```razor
<MediaQuery Query="(min-width: 768px)">
    <nav aria-label="Desktop navigation">
        ...
    </nav>
</MediaQuery>

<MediaQuery Query="(prefers-reduced-motion: reduce)">
    <p>Animations are disabled.</p>
</MediaQuery>
```

`Query` accepts any expression supported by `window.matchMedia`, including viewport, orientation, pointer, color-scheme, and reduced-motion queries. It is required and cannot be blank.

The component initially renders its wrapper with no child content, then updates after browser interop reports the first match. This avoids guessing browser state during server prerendering but can cause content to appear after hydration. Do not use it as the only authorization or data-access control; it changes rendering based on client presentation state.

The listener remains active, so the content updates when the query result changes. Changing `Query` replaces the old listener. Component disposal removes both the browser listener and its DOM observer.

## One-time check

Inject `IMediaQueryInterop` when code needs the current result without rendering the component:

```razor
@using Soenneker.Blazor.MediaQuery.Abstract
@inject IMediaQueryInterop MediaQueries

@code {
    private bool _usesCoarsePointer;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        _usesCoarsePointer = await MediaQueries.IsMediaQueryMatched("(pointer: coarse)");
        StateHasChanged();
    }
}
```

The one-time method does not subscribe to changes. Call it only after JavaScript interop is available; use the component when ongoing updates are required.
