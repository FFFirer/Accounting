using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accounting.Web.Client.Shared;

public partial class ThemeSwitch : ComponentBase
{
    [Inject]
    private IThemeService? Theme { get; set; }

    [Inject]
    private IJSRuntime? JS { get; set; }

    [Parameter]
    public string ThemeName { get; set; } = "dark";

    [Parameter]
    public bool ThemeEnabled { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        if (RendererInfo.IsInteractive)
        {

            if (JS != null)
            {
                var theme = await JS.InvokeAsync<string>("blazorInterop.readCookie", "theme");
                if (string.IsNullOrWhiteSpace(theme))
                {
                    Theme!.CurrentTheme = theme;
                }
            }
            else
            {

            }
        }

        switch (RendererInfo.Name)
        {
            default:
                break;
        }


        this.ThemeEnabled = Theme?.CurrentTheme == this.ThemeName;
    }

    private async Task ToggleTheme()
    {
        Theme?.ToggleTheme();
        // 将主题值写入Cookie
        if(JS is not null)
        {
            await JS.InvokeVoidAsync("blazorInterop.setCookie", "theme", Theme?.CurrentTheme, 30);
        }
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {

        }
        return Task.CompletedTask;
    }
}