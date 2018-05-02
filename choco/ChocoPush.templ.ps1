param(
	[string]$version = "1.0.0"
)

$pkgName = "dotnet-version." +$version +".nupkg"

choco apiKey -k <replace-with-api-key> -source https://push.chocolatey.org/
choco push $pkgName