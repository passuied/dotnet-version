using AutoFixture.AutoMoq;
using DotNet.Versioning.Core;
using System;
using System.IO;
using Xunit;

namespace DotNet.Versioning.Tests
{
    public class DotNetVersionManagerFacts
    {
        public class TheVersionMethod
        {

            [Theory]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject1\TestProject1.csproj", VersionCommandType.Major, "2.0.0")]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject1\TestProject1.csproj", VersionCommandType.Minor, "1.3.0")]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject1\TestProject1.csproj", VersionCommandType.Patch, "1.2.4")]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject2\TestProject2.csproj", VersionCommandType.Major, "2.0.0")]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject2\TestProject2.csproj", VersionCommandType.Minor, "1.1.0")]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject2\TestProject2.csproj", VersionCommandType.Patch, "1.0.1")]
            public void GivenCommand_SetVersion_AndSaveFile(string path, string command, string expVersion, string testFile)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var cpPath = Path.Combine(Directory.GetCurrentDirectory(), $"{testFile}.csproj");
                try
                {
                    File.Copy(absPath, cpPath);
                    var sut = new DotNetVersionManager(cpPath);

                    sut.Version(command);

                    new CsprojFile(cpPath).AssemblyVersion.ShouldBe(expVersion);
                }
                finally
                {
                    if (File.Exists(cpPath))
                    {
                        File.Delete(cpPath);
                    }
                }

            }
            
        }

       
    }
}
