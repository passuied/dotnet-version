using DotNet.Versioning.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace DotNet.Versioning.Tests
{
    public class CsprojFinderFacts
    {
        public class TheFindMethod
        {
            [Theory]
            [InlineData(@"..\..\..\..\..\testdata\TestProject1\TestProject1.csproj")]
            public void GivenPathCsprojFile_ReturnPath(string path)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var sut = new CsprojFileFinder(absPath);
                sut.Find();
                sut.CsprojFiles.Count().ShouldBe(1);
                sut.CsprojFiles.ElementAt(0).ShouldBe(absPath);
            }

            [Theory]
            [InlineData(@"..\..\..\..\..\testdata")]
            public void GivenPath_ReturnChildCsprojFiles(string path)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var sut = new CsprojFileFinder(absPath);
                sut.Find();
                sut.CsprojFiles.Count().ShouldBe(3);
                var f0 = sut.CsprojFiles.ElementAt(0);
                f0.Substring(f0.LastIndexOf('\\') + 1).ShouldBe("InvalidTestProject.csproj");

                var f1 = sut.CsprojFiles.ElementAt(1);
                f1.Substring(f1.LastIndexOf('\\') + 1).ShouldBe("TestProject1.csproj");

                var f2 = sut.CsprojFiles.ElementAt(2);
                f2.Substring(f2.LastIndexOf('\\') + 1).ShouldBe("TestProject2.csproj");
            }
        }
    }
}
