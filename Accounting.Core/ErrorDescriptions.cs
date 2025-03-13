using System;

using Accounting.Data;

namespace Accounting;

public class ErrorDescriptions
{
    public Error InvalidUploadToken() => new Error(nameof(InvalidUploadToken));

    public Error NotExistsUploadFileId() => new Error(nameof(NotExistsUploadFileId));
}
