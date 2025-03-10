using System;

namespace Accounting.Email;

public class EmailOptions
{
    public string? Server { get; set; }
    public int Port { get; set; } = 0;
    public string? EmailAddress { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
