using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Versioning.Core.Models
{
    public class VersionResponse
    {
        public IReadOnlyCollection<VersionResponseItem> Items { get; set; }
    }

    public class VersionResponseItem
    {
        public string CsprojFile { get; set; }
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }

        public string ErrorMessage { get; set; }

    }
}
