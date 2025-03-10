$directories = @("./bin", "./publish", "./node_modules", "./dist")

dotnet clean './Accounting.sln'

$directories | Remove-Item $_ -Recurse -Force