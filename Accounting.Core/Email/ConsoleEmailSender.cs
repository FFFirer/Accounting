using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Accounting.Email;

public sealed class ConsoleEmailSender<TUser> : IEmailSender<TUser> where TUser : class
{
    private readonly ILogger _logger;
    public ConsoleEmailSender(ILogger<ConsoleEmailSender<TUser>> logger) {
        _logger = logger;
    }

    public Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink)
    {
        _logger.LogDebug("[{Email}] email confirmation link: {Link} ", email, confirmationLink);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
    {
        _logger.LogDebug("[{Email}] password reset code: {Code}", email, resetCode);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
    {
        _logger.LogDebug("[{Email}] password reset link: {Link}", email, resetLink);
        return Task.CompletedTask;
    }
}
