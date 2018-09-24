# dotnet-version
.NET CLI equivalent to npm version.

## Synopsis
- dotnet-version CLI facilitates versioning one or multiple csproj files directly from the dotnet CLI.
- When run within a particular folder, it will locate all csproj under this folder and apply the `<AssemblyVersion>` node within the csproj file.
- dotnet-version follows semver 2.0 (see https://semver.org/)

## Install
- As of version 1.2, dotnet-version can now be installed as a .NET tool global tool
  ```
  > dotnet tool install DotNetVersioning.Tool -g
  ```
- Previous versions
    - dotnet-version is available via Chocolatey (https://chocolatey.org/)
    1. Follow instructions about installing Chocolatey [here](https://chocolatey.org/install)
    2. Run the following command
    ```
    > choco install dotnet-version
    ```

## Usage
```
dotnet version [<version> | major | minor | patch | build]
```
- When using `<version>`, all csproj files will be udpated to the given <version>
- When using `major`, all csproj files will be upgraded to the next major version
- When using `minor`, all csproj files will be upgraded to the next minor version
- When using `patch`, all csproj files will be upgraded to the next patch version
- When using `build`, all csproj files will be upgraded to the next build version

