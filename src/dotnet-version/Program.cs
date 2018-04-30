using DotNet.Versioning.Core;
using System;
using System.IO;

namespace DotNet.Versioning
{
    class Program
    {
        // dotnet-version <version> | major | minor | patch
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowHelp();
                return;
            }
            try
            {
                var command = args[0];
                var versionManager = new DotNetVersionManager(Directory.GetCurrentDirectory());
                versionManager.Version(command);
            }
            catch (ArgumentException aex)
            {
                Console.WriteLine($"Error: {aex.Message}");
                return;
            }

        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"Command: dotnet-version <version> | major | minor | patch");
        }
    }
}
