﻿
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Accounting.Web.Common;

public class ServerHostEnvironment(IWebHostEnvironment env, NavigationManager nav) :
    IWebAssemblyHostEnvironment
{
    public string Environment => env.EnvironmentName;
    public string BaseAddress => nav.BaseUri;
}
