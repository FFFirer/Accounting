[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $Version = "1.0.0",

    # build
    [Parameter()]
    [switch]
    $Build,

    # push
    [Parameter()]
    [switch]
    $Push
)

$Registry = "registry.private.fffirer.top:9999"
$AppImageName = "accounting"
$App_Dockerfile = "Dockerfile"
$MigratorImageName = "accounting-migrator"
$Migrator_Dockerfile = "Migrator.Dockerfile"

$App_TAG = "$($Registry)/$($AppImageName):$($Version)"
$App_TAG_Latest = "$($Registry)/$($AppImageName):latest"

$Migrator_TAG = "$($Registry)/$($MigratorImageName):$($Version)"
$Migrator_TAG_Latest = "$($Registry)/$($MigratorImageName):latest"

if ($Build) {
    docker build -t "$($App_TAG)" -f $App_Dockerfile .

    docker build -t "$($Migrator_TAG)" -f $Migrator_Dockerfile .
}

if ($Push) {
    docker tag "$($App_TAG)" "$($App_TAG_Latest)"; Write-Host "Tagging $($App_TAG) to $($App_TAG_Latest)";
    docker tag "$($Migrator_TAG)" "$($Migrator_TAG_Latest)"; Write-Host "Tagging $($Migrator_TAG) to $($Migrator_TAG_Latest)";
    docker push "$($App_TAG)"
    docker push "$($App_TAG_Latest)"
    docker push "$($Migrator_TAG)"
    docker push "$($Migrator_TAG_Latest)"
}