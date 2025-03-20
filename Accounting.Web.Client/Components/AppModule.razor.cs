using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accounting.Web.Client.Components;

public partial class AppModule : ComponentBase
{
    [Inject]
    private IJSRuntime? js { get; set; }

    [Inject]
    private ILogger<AppModule>? logger { get; set; }

    [Parameter]
    public string? AppRoute { get; set; }

    [Parameter]
    public string? ModuleFilePath { get; set; }

    [Parameter]
    public string? RenderFuncName { get; set; }

    public ElementReference? elementReference { get; set; }
    public IJSObjectReference? module { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        logger?.LogDebug("[OnAfterRender] Route:{AppRoute}, {FirstRender}", AppRoute, firstRender) ;

        try
        {
            if (firstRender)
            {
                var module = await js!.InvokeAsync<IJSObjectReference>("import", ModuleFilePath);
                await module.InvokeVoidAsync(RenderFuncName!, elementReference);
            }
        }
        catch (System.Exception ex)
        {
            logger?.LogError(ex, "Run {RenderFuncName} Failed", RenderFuncName);
        }
    }
}