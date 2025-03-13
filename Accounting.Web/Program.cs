using Accounting;
using Accounting.Email;
using Accounting.Web.Components;
using Accounting.Web.Components.Account;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AccountingDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddLogging();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
}).AddIdentityCookies();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AccountingDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddEmailSender();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/Account/Login";

    var old = x.Events.OnRedirectToLogin;

    x.Events.OnRedirectToLogin = (context) =>
    {
        if (context.HttpContext.Request.Headers.TryGetValue("X-Forwarded-Proto", out var schema))
        {
            var index = context.RedirectUri.IndexOf(':');
            var redirectSchema = context.RedirectUri[0..(index + 1)];

            if (redirectSchema != schema)
            {
                context.RedirectUri = schema + context.RedirectUri[index..];
            }
        }

        return old.Invoke(context);
    };
});

builder.Services.AddAccountingCore()
    .AddFileStorage()
    .AddEntityFrameworkCoreStores();

builder.Services.AddSerilog();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseSerilogRequestLogging(
    options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        options.GetLevel = (httpContext, elapsed, ex) => ex != null ? LogEventLevel.Error : LogEventLevel.Information;
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value ?? "");
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        };
    });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

// Console.WriteLine("Hello world");

app.Run();

// Console.WriteLine("Hello world2");
