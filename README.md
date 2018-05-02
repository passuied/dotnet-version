# dotnet-version
.NET CLI equivalent to npm version.

## Synopsis
- dotnet-version CLI facilitates versioning one or multiple csproj files directly from the dotnet CLI.
- When run within a particular folder, it will locate all csproj under this folder and apply the `<AssemblyVersion>` node within the csproj file.
- dotnet-version follows semver 2.0 (see https://semver.org/)

## Install
- dotnet-version is available via Chocolatey (https://chocolatey.org/)
1. Follow instructions about installing Chocolatey [here](https://chocolatey.org/install)
2. Run the following command
```
> choco install dotnet-version
```

## Usage
```
dotnet version <version> | major | minor | patch
```
- When using `<version>`, all csproj files will be udpated to the given <version>
- When using `major`, all csproj files will be upgraded to the next major version
- When using `minor`, all csproj files will be upgraded to the next minor version
- When using `patch`, all csproj files will be upgraded to the next patch version

## Contribute
### Build and package
- Run the following command:
```
> .\build.cmd
```
- To install from local source:
  - Open a Powershell terminal as Administrator
  - Navigate to ./choco folder
  - Create package:
  ```
  > .\ChocoPack.ps1 -version <version>
  ```
  - Install package
  ```
  > .\ChocoTest.ps1
  ```
- To push an update to Chocolatey
  - Edit ./choco/ChocoPush.templ.ps1
  - Contact passuied@csod.com to get the API Key
  - Replace placeholder with API key
  - Open a Powershell terminal as Administrator
  - Navigate to ./choco folder
  ```
  > .\ChocoPush.ps1 -version <version>
  ```
- To uninstall
  - Open a Powershell terminal as Administrator
  - Navigate to ./choco folder
  ```
  > .\ChocoUninstall.ps1
  ```
  
