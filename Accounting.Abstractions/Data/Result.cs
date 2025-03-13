using System;
using System.Diagnostics.CodeAnalysis;

namespace Accounting.Data;

public class Result
{
    protected static readonly Result _success = new Result(true);
    protected bool _succeeded { get; set; }
    protected virtual List<Error> _errors { get; set; } = [];
    public virtual bool Succeeded => _succeeded;

    protected Result(bool succeeded)
    {
        _succeeded = succeeded;
    }


    public static Result Failed(params IEnumerable<Error> errors)
    {
        var result = new Result(false) { _errors = errors.ToList() };
        return result;
    }

    public static Result Success() => _success;
    public static Result<T> Success<T>(T data) => new Result<T>(data);

    public void ThrowIfFailed(string message = "发生了一些错误")
    {
        if (_errors.IsNullOrEmpty())
        {
            return;
        }

        throw new AggregateException(message, _errors.Select(x => new ResultException(x)));
    }

    public override string ToString()
    {
        return Succeeded ? "Succeeded" : "Error: " + string.Join("; ", _errors.Select(x => x.ToString()));
    }
}
