using AutoFixture.AutoMoq;
using DotNet.Versioning.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace DotNet.Versioning.Tests
{
    public class CsprojFileFacts
    {
        public class TheAssemblyVersionProperty
        {
            [Theory]
            [InlineData(@"..\..\..\..\..\testdata\TestProject1\TestProject1.csproj")]
            public void GivenAssemblyVersionExists_ReturnValue(string path)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var sut = new CsprojFile(absPath);
                sut.AssemblyVersion.ShouldBe("1.2.3");
            }

            [Theory]
            [InlineData(@"..\..\..\..\..\testdata\TestProject2\TestProject2.csproj")]
            public void GivenAssemblyVersionDoesntExist_ReturnDefault(string path)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var sut = new CsprojFile(absPath);
                sut.AssemblyVersion.ShouldBe("1.0.0");
            }

            [Theory]
            [InlineData(@"..\..\..\..\..\testdata\InvalidTestProject\InvalidTestProject.csproj")]
            public void GivenInvalidCsproj_ThrowArgumentException(string path)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                Assert.Throws<ArgumentException>(() =>new CsprojFile(absPath).AssemblyVersion);
            }

            [Theory]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject1\TestProject1.csproj")]
            public void GivenAssemblyVersionExists_SetValue_OverwritesElement(string path, string testFile)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var cpPath = Path.Combine(Directory.GetCurrentDirectory(), $"{testFile}.csproj");
                try
                {
                    File.Copy(absPath, cpPath);
                    var sut = new CsprojFile(cpPath);
                    sut.AssemblyVersion.ShouldBe("1.2.3");
                    sut.AssemblyVersion = "3.2.1";
                    sut.AssemblyVersion.ShouldBe("3.2.1");
                    sut.Save();

                    var sut2 = new CsprojFile(cpPath);
                    sut2.AssemblyVersion.ShouldBe("3.2.1");
                }
                finally
                {
                    if (File.Exists(cpPath))
                    {
                        File.Delete(cpPath);
                    }       
                }
               
            }

            [Theory]
            [InlineAutoMoqData(@"..\..\..\..\..\testdata\TestProject2\TestProject2.csproj")]
            public void GivenAssemblyVersionDoesntExist_SetValue_AddsElement(string path, string testFile)
            {
                var absPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                var cpPath = Path.Combine(Directory.GetCurrentDirectory(), $"{testFile}.csproj");
                try
                {
                    File.Copy(absPath, cpPath);
                    var sut = new CsprojFile(cpPath);
                    sut.AssemblyVersion.ShouldBe("1.0.0");
                    sut.AssemblyVersion = "1.2.3";
                    sut.AssemblyVersion.ShouldBe("1.2.3");
                    sut.Save();

                    var sut2 = new CsprojFile(cpPath);
                    sut2.AssemblyVersion.ShouldBe("1.2.3");
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
