using System;
using MediatR;

namespace Accounting.Imports;

public class UpdateImportRecordStatusNotification : INotification
{
    public long ImportRecordId { get; set; }
    public ImportStatus From { get; set; }
    public ImportStatus To { get; set; }
}
