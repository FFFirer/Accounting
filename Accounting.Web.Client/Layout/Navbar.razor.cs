using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Accounting.Web.Client.Layout;

public partial class Navbar : ComponentBase
{
    [Inject]
    private IWebAssemblyHostEnvironment? Env { get; set; }
}
