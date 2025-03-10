$directories = @("./bin", "./publish", "./node_modules", "./dist")

dotnet clean './Accounting.sln'

$directories | ForEach-Object {
    if(Test-Path $_) {
        Remove-Item $_ -Recurse -Force
        Write-Host "clean: $($_)"
    }
} 