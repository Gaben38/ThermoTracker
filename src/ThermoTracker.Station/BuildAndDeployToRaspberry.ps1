function Invoke-Call {
    param (
        [scriptblock]$ScriptBlock,
        [string]$ErrorAction = $ErrorActionPreference
    )
    & @ScriptBlock
    if (($lastexitcode -ne 0) -and $ErrorAction -eq "Stop") {
        exit $lastexitcode
    }
}

cd $PSScriptRoot
Invoke-Call -ScriptBlock { dotnet build --configuration Release --nologo } -ErrorAction Stop
Invoke-Call -ScriptBlock { dotnet publish -p:PublishProfile=FolderProfile --nologo } -ErrorAction Stop
Invoke-Call -ScriptBlock { docker context use remote } -ErrorAction Stop
Invoke-Call -ScriptBlock { docker build -t thermo-tracker-station-image -f Dockerfile . } -ErrorAction Stop
#Invoke-Call -ScriptBlock { docker create --name thermo-tracker-station thermo-tracker-station-image } -ErrorAction Stop