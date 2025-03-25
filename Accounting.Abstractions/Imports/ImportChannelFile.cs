using MediatR;

namespace Accounting.Imports;

public class ImportChannelFileNotification : INotification
{
    public ImportRecord? Record { get; set; }
}
