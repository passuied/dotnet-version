using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNet.Versioning.Core
{
    public class CsprojFileFinder : ICsprojFileFinder
    {
        private readonly string _path;
        private List<string> _csprojFiles;


        public IEnumerable<string> CsprojFiles
        {
            get { return _csprojFiles; }
        }

        public CsprojFileFinder(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            this._path = path.Trim();


            if (!Directory.Exists(_path) && !File.Exists(_path))
            {
                throw new ArgumentException($"Invalid Path '{_path}'");
            }

            _csprojFiles = new List<string>();
        }

        public void Find()
        {
            if (_path.ToLowerInvariant().EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
            {
                _csprojFiles.Add(_path);
            }
            else
            {
                _csprojFiles.AddRange(Directory.GetFiles(_path, "*.csproj", SearchOption.AllDirectories));
            }

        }
    }
}
