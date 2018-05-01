using DotNet.Versioning.Core.Models;
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

        public VersionResponse Version(string command)
        {
            var response = new VersionResponse();
            var items = new List<VersionResponseItem>();
            response.Items = items;
            foreach(var csprojFile in _csprojFiles)
            {
                try
                {
                    var csproj = new CsprojFile(csprojFile);
                    var oldVersion = VersionInfo.Parse(csproj.AssemblyVersion);
                    var newVersion = oldVersion.Up(command);
                    csproj.AssemblyVersion = newVersion.ToString();

                    csproj.Save();

                    items.Add(new VersionResponseItem
                    {
                        CsprojFile = csprojFile,
                        OldVersion = oldVersion.ToString(),
                        NewVersion = newVersion.ToString(),

                    });
                }
                catch(ArgumentException exc)
                {
                    items.Add(new VersionResponseItem
                    {
                        CsprojFile = csprojFile,
                        ErrorMessage = exc.Message
                    });
                }
            }

            return response;
        }

        


    }
}
