using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Accounting.Data;

public class Result
{
    [JsonIgnore]
    protected static readonly Result _success = new Result(true);

    [JsonIgnore]
    protected bool _succeeded { get; set; }

    [JsonIgnore]
    protected virtual List<Error> _errors { get; set; } = [];

    public IEnumerable<Error> Errors => _errors;

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
