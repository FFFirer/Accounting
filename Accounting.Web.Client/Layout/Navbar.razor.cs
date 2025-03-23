using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace Accounting.Web.Client.Layout;

public partial class Navbar : ComponentBase, IDisposable
{
    [Inject]
    private IWebAssemblyHostEnvironment Env { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private ILogger<Navbar> logger { get; set; } = default!;

    [Inject]
    private IJSRuntime JS {get;set;} = default!;

    public void Dispose()
    {
        this.NavigationManager.LocationChanged -= LocationChangedHandler;
    }

    protected override Task OnInitializedAsync()
    {
        if (this.NavigationManager is not null)
        {
            NavigationManager.LocationChanged += LocationChangedHandler;
        }

        return Task.CompletedTask;
    }

    private void LocationChangedHandler(object? sender, LocationChangedEventArgs e)
    {
        this.JS.InvokeVoidAsync("blazorInterop.autoBlur");
        // InvokeAsync(StateHasChanged);
    }
}
