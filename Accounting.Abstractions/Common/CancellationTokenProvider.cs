using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Common;

public interface ICancellationTokenProvider
{
    CancellationToken Token { get; }
}

public class CancellationTokenProvider : ICancellationTokenProvider
{
    private readonly CancellationToken _cancellationToken;
    public CancellationTokenProvider(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken ?? CancellationToken.None;
    }

    public CancellationToken Token => _cancellationToken;
}
