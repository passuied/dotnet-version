using System;
using System.Collections.Generic;

namespace DotNet.Versioning.Core
{
    public class DotNetVersionManager
    {
        private IEnumerable<string> _csprojFiles;

        public DotNetVersionManager(ICsprojFileFinder csprojFileFinder)
        {
            csprojFileFinder.Find();
            this._csprojFiles = csprojFileFinder.CsprojFiles;
        }

        public DotNetVersionManager(string path)
            : this(new CsprojFileFinder(path))
        {
        }

        public void Version(string command)
        {
            foreach(var csprojFile in _csprojFiles)
            {
                var csproj = new CsprojFile(csprojFile);

                var v = VersionInfo.Parse(csproj.AssemblyVersion);
                v = v.Up(command);
                csproj.AssemblyVersion = v.ToString();

                csproj.Save();
            }
        }

        


    }
}
