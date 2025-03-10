using System;

using MailKit;
using MailKit.Net.Smtp;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MimeKit;

namespace Accounting.Email;

public class EmailSender : IEmailSender
{
    protected virtual IOptionsMonitor<EmailOptions> _options { get; set; }
    protected virtual ILogger _logger { get; set; }

    public EmailSender(IOptionsMonitor<EmailOptions> options, ILogger<EmailSender> logger)
    {
        _options = options;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.CurrentValue?.UserName ?? _options.CurrentValue?.EmailAddress, _options.CurrentValue?.EmailAddress));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder()
        {
            HtmlBody = htmlMessage
        };


        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_options.CurrentValue?.Server, _options.CurrentValue!.Port);
            await client.AuthenticateAsync(_options.CurrentValue?.EmailAddress, _options.CurrentValue?.Password);
            await client.SendAsync(message, progress: new TransferProgressLogger(_logger));
            await client.DisconnectAsync(true);
        }
    }

    private sealed class TransferProgressLogger : ITransferProgress
    {
        private readonly ILogger _logger;

        public TransferProgressLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Report(long bytesTransferred, long totalSize)
        {
            _logger.LogDebug("Transferred/TotalSize: {Transferred}/{TotalSize}", bytesTransferred, totalSize);
        }

        public void Report(long bytesTransferred)
        {
            _logger.LogDebug("Transferred: {Transferred}", bytesTransferred);
        }
    }
}
