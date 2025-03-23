using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace Accounting.Web.Client.Layout;

public partial class Navbar : ComponentBase
{
    [Inject]
    private IWebAssemblyHostEnvironment Env { get; set; } = default!;
}
