using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Accounting.Email;

public class IdentityEmailSender<TUser> : IEmailSender<TUser> where TUser : class
{
    protected virtual IEmailSender Sender { get; set; }
    public IdentityEmailSender(IEmailSender sender)
    {
        Sender = sender;
    }

    public Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink)
    {
        return Sender.SendEmailAsync(email, "确认是你的邮箱", $"请<a href='{confirmationLink}'>点击这里</a>确认这是你的邮箱。");
    }

    public Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        return Sender.SendEmailAsync(email, "重置密码", $"请使用以下Code重置你的密码：{resetCode}。");
    }

    public Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        return Sender.SendEmailAsync(email, "重置密码", $"请使用以下链接重置你的密码：<a href='{resetLink}'>点击这里</a>。");
    }
}
