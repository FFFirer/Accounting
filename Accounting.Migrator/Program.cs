using System.CommandLine;

using Accounting.Migrator;

var root = new Root();

await root.InvokeAsync(args);