using Microsoft.AspNetCore.Components.Routing;

namespace Accounting.Web.Client.Components;

public partial class NavLink2 : NavLink
{
    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        // TODO: 判断运行环境，如果是WebAssembly且禁用blazor-enhance-nav，使用js监听路由变动
        if (firstRender)
        {
            if (this.AdditionalAttributes?.TryGetValue("data-enhance-nav", out var disabled) ?? false)
            {
                
            }
        }

        return Task.CompletedTask;
    }
}