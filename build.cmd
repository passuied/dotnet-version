dotnet restore --source http://scm.csod.com/Nuget/nuget --source https://api.nuget.org/v3/index.json dotnet-version.sln
dotnet build -c Release --no-restore src/DotnetVersioning.Console
dotnet publish -c Release -o ../../dist/  src/DotnetVersioning.Console
cd dist
"C:\Program Files\7-Zip\7z.exe" a ../artifacts/artifact.zip *.*

