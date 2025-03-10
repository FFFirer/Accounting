using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Accounting.Email;

public static class IdentityBuilderExtensions
{
    public static IdentityBuilder AddEmailSender(this IdentityBuilder builder, Type? emailSenderType = null)
    {
        builder.Services.AddTransient<IConfigureOptions<EmailOptions>, ConfigureEmailOptions>();

        emailSenderType ??= typeof(ConsoleEmailSender<>);

        // 构造类型
        var serviceType = typeof(IEmailSender<>).MakeGenericType(builder.UserType);
        var implType = emailSenderType.MakeGenericType(builder.UserType);

        builder.Services.Add(ServiceDescriptor.Singleton(serviceType, implType));
        builder.Services.AddSingleton<IEmailSender, EmailSender>();

        return builder;
    }
}
