using System;

namespace Accounting.Data;

public record Error(string Code, string? Description = null)
{
    public override string ToString()
    {
        if (this.Description is null)
        {
            return this.Code;
        }

        return this.Code + ": " + this.Description;
    }
}