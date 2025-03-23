using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Accounting.Data;

public class Result<T> : Result
{
    [JsonIgnore]
    private readonly T? _data;

    public T? Data => _data;

    public Result(T data) : base(true)
    {
        this._data = data;
    }

    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(true, nameof(_data))]
    public override bool Succeeded {
        get { return _succeeded; }
    }

    protected Result(bool succeeded) : base(succeeded)
    {

    }

    public new static Result<T> Failed(params IEnumerable<Error> errors)
    {
        return new Result<T>(false) { _errors = errors.ToList() };
    }

    public static explicit operator T(Result<T> result)
    {
        result.ThrowIfFailed();

        return result.Data!;
    }

    public static implicit operator Result<T>(T data) => Result.Success(data);
}


