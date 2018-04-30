using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Versioning.Core
{
    /// <summary>
    /// Version Info
    /// </summary>
    public class VersionInfo
    {
        public VersionInfo(int major, int minor=0, int patch=0, string prerelease=null)
        {
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
            this.Prerelease = prerelease;
        }
        public int Major { get; private set; }

        public int Minor { get; private set; }

        public int Patch { get; private set; }

        public string Prerelease { get; private set; }

        public VersionInfo Up(string command)
        {
            int major = this.Major;
            int minor = this.Minor;
            int patch = this.Patch;
            switch (command.ToLowerInvariant())
            {
                case VersionCommandType.Major:
                    {
                        major++;
                        minor = 0;
                        patch = 0;
                    }
                    break;
                case VersionCommandType.Minor:
                    {
                        minor++;
                        patch = 0;
                    }
                    break;
                case VersionCommandType.Patch:
                    {
                        patch++;
                    }
                    break;
                default:
                    if (TryParse(command, out var result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid version or command '{command}'", nameof(command));
                    }

            }
            return new VersionInfo(major, minor, patch);
        }

        
        private static readonly Regex _rgxVersion = new Regex(@"^(?<major>\d+)(\.(?<minor>\d+))?(\.(?<patch>\d+))?(\-(?<pre>[0-9A-Za-z\-\.]+))?$", RegexOptions.IgnoreCase|RegexOptions.Compiled|RegexOptions.CultureInvariant);

        public static bool TryParse(string version, out VersionInfo result)
        {
            result = null;
            var m = _rgxVersion.Match(version);

            if (m.Success)
            {
                var major = Convert.ToInt32(m.Groups["major"].Value);

                var mMinor = m.Groups["minor"];
                var minor = 0;
                if (mMinor.Success)
                {
                    minor = Convert.ToInt32(mMinor.Value);
                }

                var mPatch = m.Groups["patch"];
                var patch = 0;
                if (mPatch.Success)
                {
                    patch = Convert.ToInt32(mPatch.Value);
                }

                string pre = null;
                var mPre = m.Groups["pre"];
                if (mPre.Success)
                {
                    pre = mPre.Value;
                }

                result = new VersionInfo(major, minor, patch, pre);
                return true;

            }
            return false;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}{(!string.IsNullOrEmpty(Prerelease)?'-'+Prerelease: string.Empty)}";
        }
        public static VersionInfo Parse(string version)
        {
            VersionInfo result;
            if (TryParse(version, out result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid Format. Expected: <major>[.<minor>][.<patch>][-<pre>]", nameof(version));
            }
        }
    }
}
