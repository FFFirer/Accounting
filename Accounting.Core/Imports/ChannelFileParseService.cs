using Accounting.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Imports;

public class ChannelFileParseService
{
    protected virtual IServiceProvider ServiceProvider { get; set; }

    public ChannelFileParseService(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }

    public async Task<Result> ParseAsync(ImportRecord record, CancellationToken cancellationToken)
    {
        IChannelFileParser parser = GetChannelFileParser(record);

        return await parser.ParseAsync(record, cancellationToken);
    }

    private IChannelFileParser GetChannelFileParser(ImportRecord record)
    {
        if (record.Channel is null)
        {
            throw new InvalidOperationException("未指定渠道");
        }

        return this.ServiceProvider.GetRequiredKeyedService<IChannelFileParser>(record.Channel.Code);
    }
}