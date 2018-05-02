$scriptPath =  $(Split-Path $MyInvocation.MyCommand.Path)
	
$appFolder = "$env:systemdrive\tools\dotnet-version"

$packageArgs = @{
  packageName = 'dotnet-version'
  file = Join-Path $scriptPath 'artifact.zip'
  unzipLocation = $appFolder
}

Install-ChocolateyZipPackage @packageArgs


Install-ChocolateyPath -PathToInstall $appFolder -PathType 'Machine'



