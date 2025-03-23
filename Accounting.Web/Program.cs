using Accounting;
using Accounting.Common;
using Accounting.Email;
using Accounting.Quartz;
using Accounting.Quartz.Endpoints;
using Accounting.Web;
using Accounting.Web.Client;
using Accounting.Web.Common;
using Accounting.Web.Components;
using Accounting.Web.Components.Account;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Quartz;
using Quartz.Impl.AdoJobStore;

using Serilog;
using Serilog.Events;

const string DefaultConnectionName = "Default";
const string QuartzConnectionName = "Quartz";

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddProblemDetails();

builder.Services
    .AddDbContext<AccountingDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString(DefaultConnectionName)));

builder.Services
    .AddDbContext<AccountingQuartzDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString(QuartzConnectionName)));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddOpenApiDocument();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLogging(options =>
{
    options.AddSerilog(Log.Logger);
});
builder.Services.AddScoped<ICancellationTokenProvider>(sp => new CancellationTokenProvider(sp.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted));

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAntiforgery(options => { });
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

// builder.Services.AddHttpLogging(options =>
// {
//     options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders;
// });

// Quartz
builder.Services.Configure<QuartzOptions>(builder.Configuration.GetSection(nameof(Quartz)));
builder.Services
    .AddQuartz(
        config =>
        {
            config.UsePersistentStore(p =>
            {
                p.UseProperties = true;
                p.UsePostgres(postgresOptions =>
                {
                    postgresOptions.UseDriverDelegate<PostgreSQLDelegate>();
                    postgresOptions.ConnectionString = builder.Configuration.GetConnectionString(QuartzConnectionName)!;
                    postgresOptions.TablePrefix = "quartz.qrtz_";
                });
                p.UseSystemTextJsonSerializer();
            });
        })
    .AddQuartzHostedService(
        options =>
        {
            options.WaitForJobsToComplete = true;
        })
    .AddAccountingJobs();

builder.Services.Configure<ForwardedHeadersOptions>(
    options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    }
);

builder.Services.AddAccountingCore()
    .AddFileStorage()
    .AddEntityFrameworkCoreStores();

builder.Services.AddScoped<IThemeService, AspNetThemeService>();
builder.Services.AddScoped<IWebAssemblyHostEnvironment, ServerHostEnvironment>();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseForwardedHeaders();

// app.UseHttpLogging();

// app.Use(async (context, next) =>
// {
//     app.Logger.LogInformation("Request RemoteIp: {RemoteIpAndPort}", context.Connection.RemoteIpAddress);
//     await next(context);
// });

// app.UseSerilogRequestLogging(
//     options =>
//     {
//         options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
//         options.GetLevel = (httpContext, elapsed, ex) => ex != null ? LogEventLevel.Error : LogEventLevel.Information;
//         options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
//         {
//             diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value ?? "");
//             diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
//         };
//     });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseRouting();

app.UseAuthentication();    // 在UseRouting和UseEndpoints之间
app.UseAuthorization();

app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Accounting.Web.Client._Imports).Assembly);

app.MapAccountingQuartzApiEndpoints();
app.MapAdditionalIdentityEndpoints().WithTags("Account");

app.Run();
