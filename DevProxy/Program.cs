using DevProxy;

using Serilog;
using Serilog.Events;

using Yarp.ReverseProxy.Transforms;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(buildContext =>
    {
        if(buildContext.Route.Match.Path == "/frontend/index.{css,js}")
        {
            buildContext.AddRequestTransform(CustomRequestTransforms.RemoveFileHash);
        }
    });

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

app.MapReverseProxy();

app.Run();
