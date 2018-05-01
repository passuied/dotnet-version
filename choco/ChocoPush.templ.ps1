param(
	[string]$version = "1.0.0"
)

$pkgName = "dotnetversion." +$version +".nupkg"

choco apiKey -k <replace-with-api-key> -source https://push.chocolatey.org/
choco push $pkgName