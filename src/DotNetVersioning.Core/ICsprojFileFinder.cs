using System.Collections.Generic;

namespace DotNet.Versioning.Core
{
    public interface ICsprojFileFinder
    {
        IEnumerable<string> CsprojFiles { get; }

        void Find();
    }
}