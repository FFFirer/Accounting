using DevProxy;

using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(buildContext =>
    {
        if(buildContext.Route.Match.Path == "/app/index.{css,js}")
        {
            buildContext.AddRequestTransform(CustomRequestTransforms.RemoveFileHash);
        }
    });

var app = builder.Build();

app.MapReverseProxy();

app.Run();
