[![](https://img.shields.io/nuget/v/soenneker.blazor.mediaquery.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.mediaquery/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.mediaquery/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.mediaquery/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.mediaquery.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.mediaquery/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.MediaQuery
### A Blazor interop library for media queries and viewport size logic

## Installation

```
dotnet add package Soenneker.Blazor.MediaQuery
```

### Register MediaQuery with your `IServiceCollection`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMediaQuery();
}
```

## Usage

```razor
<MediaQuery Query="(min-width: 768px)"> // Supports standard CSS media queries
    This is visible at widths greater than 768px
</MediaQuery>
```