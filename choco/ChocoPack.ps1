param(
	[string]$version = "1.0.0"
)


choco pack ../src/DotNetVersioning.Choco/dotnetversion.nuspec --version=$version



