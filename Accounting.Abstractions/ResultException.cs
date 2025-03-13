using System;

using Accounting.Data;

namespace Accounting;

public class ResultException : Exception
{
    public ResultException(Error error) : base(error.ToString())
    {

    }
}
