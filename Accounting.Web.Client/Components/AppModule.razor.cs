using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accounting.Web.Client.Components;

public partial class AppModule : ComponentBase
{
    [Inject]
    private IJSRuntime? js { get; set; }

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
        if (firstRender)
        {
            var module = await js!.InvokeAsync<IJSObjectReference>("import", ModuleFilePath);
            await module.InvokeVoidAsync(RenderFuncName!, elementReference);
        }
    }
}