param(
	[string]$version = "1.0.0"
)


choco pack ../src/DotNetVersioning.Choco/dotnet-version.nuspec --version=$version



