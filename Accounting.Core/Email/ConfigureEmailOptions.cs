using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Accounting.Email;

public class ConfigureEmailOptions : IConfigureOptions<EmailOptions>
{
    protected virtual string SectionName { get; } = nameof(EmailOptions);
    protected virtual IConfiguration Configuration { get; set; }

    public ConfigureEmailOptions(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void Configure(EmailOptions options)
    {
        this.Configuration.GetSection(SectionName)?.Bind(options);
    }
}
