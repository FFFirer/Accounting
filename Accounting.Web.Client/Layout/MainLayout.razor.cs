using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Accounting.Web.Client.Layout;

public partial class MainLayout : LayoutComponentBase, IAsyncDisposable
{

    [Inject]
    private IWebAssemblyHostEnvironment? Env { get; set; }
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    static NavModel SwaggerNav = new("/swagger", "OpenAPI Docs");
    static ICollection<NavModel> MainNavs = [new("/Dashboard", "仪表板"), new("/Ledgers", "账本管理"), new("/AssetAccounts",
"资产账户")];

    static ICollection<NavModel> TaskNavs = [new("/Tasks/#", "任务定义"), new("/Tasks/#", "任务实例"), new("/Tasks/#", "任务日志")];
    static ICollection<NavModel> AppTasksNavs = [new("/app/Tasks/Jobs", "任务定义", true), new("/app/Tasks/JobDetails", "任务详情", true), new("/app/Tasks/Triggers", "触发器", true)];

    public ICollection<NavModel> DefaultNavs { get; set; } = [];

    protected override Task OnInitializedAsync()
    {
        this.DefaultNavs = GetNavsFromUri(this.NavigationManager?.Uri);

        if(this.Env?.Environment == "Development") {
            this.DefaultNavs =  this.DefaultNavs.Concat([SwaggerNav]).ToList();
        }

        if (this.NavigationManager is not null)
        {
            this.NavigationManager.LocationChanged += ChangedLocation;
        }

        return Task.CompletedTask;
    }

    private ICollection<NavModel> GetNavsFromUri(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) { return MainNavs; }

        var uri = new Uri(url);

        if (uri.PathAndQuery.StartsWith("/Tasks"))
        {
            return TaskNavs;
        }

        if (uri.PathAndQuery.StartsWith("/app/Tasks"))
        {
            return AppTasksNavs;
        }

        return MainNavs;
    }

    private void ChangedLocation(object? sender, LocationChangedEventArgs e)
    {
        Console.WriteLine("route changed, {0}", e.Location);
    }

    public async ValueTask DisposeAsync()
    {
        if (NavigationManager is not null)
        {
            NavigationManager.LocationChanged -= ChangedLocation;
        }

        await ValueTask.CompletedTask;
    }
}
