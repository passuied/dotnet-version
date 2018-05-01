using DotNet.Versioning.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotNet.Versioning.Tests
{
    public class VersionInfoFacts
    {
        public class TheTryParseMethod
        {
            [Theory]
            [InlineData("1", "1.0.0")]
            [InlineData("1.1", "1.1.0")]
            [InlineData("1.1.0", "1.1.0")]
            [InlineData("1.1.0-pre1", "1.1.0-pre1")]
            [InlineData("1.1-pre1", "1.1.0-pre1")]
            [InlineData("1-pre1", "1.0.0-pre1")]
            public void GivenValidVersion_ReturnsTrueAndVersionInfo(string input, string output)
            {
                VersionInfo.TryParse(input, out var result).ShouldBeTrue();
                result.ToString().ShouldBe(output);
            }

            [Theory]
            [InlineData("a")]
            [InlineData("1.a")]
            [InlineData("1.1.a")]
            [InlineData("1.1.1a")]
            [InlineData("1.1.1-|")]
            public void GivenInvalidVersion_ReturnsFalse(string input)
            {
                VersionInfo.TryParse(input, out var result).ShouldBeFalse();
            }
        }

        public class TheParseMethod
        {
            [Theory]
            [InlineData("1", "1.0.0")]
            [InlineData("1.1", "1.1.0")]
            [InlineData("1.1.0", "1.1.0")]
            [InlineData("1.1.0-pre1", "1.1.0-pre1")]
            [InlineData("1.1-pre1", "1.1.0-pre1")]
            [InlineData("1-pre1", "1.0.0-pre1")]
            public void GivenValidVersion_ReturnsVersionInfo(string input, string output)
            {
                VersionInfo.Parse(input).ToString().ShouldBe(output);
            }

            [Theory]
            [InlineData("a")]
            [InlineData("1.a")]
            [InlineData("1.1.a")]
            [InlineData("1.1.1a")]
            [InlineData("1.1.1-|")]
            public void GivenInvalidVersion_ThrowsInvalidException(string input)
            {
                var exc = Assert.Throws<ArgumentException>(() => VersionInfo.Parse(input));
                exc.ParamName.ShouldBe("version");
            }
        }

        public class TheUpMethod
        {
            [Theory]
            [InlineData("0.0.1", VersionCommandType.Major, "1.0.0")]
            [InlineData("0.1.1", VersionCommandType.Major, "1.0.0")]
            [InlineData("1.0.0", VersionCommandType.Major, "2.0.0")]
            [InlineData("1.0.0", VersionCommandType.Minor, "1.1.0")]
            [InlineData("1.0.0", VersionCommandType.Patch, "1.0.1")]
            [InlineData("1.1.0", VersionCommandType.Major, "2.0.0")]
            [InlineData("1.0.1", VersionCommandType.Minor, "1.1.0")]
            [InlineData("1.1.1-pre1", VersionCommandType.Patch, "1.1.2")]
            [InlineData("1-pre1", VersionCommandType.Patch, "1.0.1")]
            [InlineData("1.0.0", "1.2.3", "1.2.3")]
            public void GivenVersion_AndCommand_UpgradeVersion(string fromVersion, string command, string expVersion)
            {
                VersionInfo.Parse(fromVersion).Up(command).ToString().ShouldBe(expVersion);
            }

            [Theory]
            [InlineData("1.0.0", "a.b.c")]
            public void GivenVersion_AndInvalidCommand_ThrowArgumentException(string fromVersion, string command)
            {
                var exc = Assert.Throws<ArgumentException>(() => VersionInfo.Parse(fromVersion).Up(command));
            }
        }
    }
}
