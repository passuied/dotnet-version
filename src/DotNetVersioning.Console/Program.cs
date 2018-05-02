using DotNet.Versioning.Core;
using System;
using System.IO;
using System.Linq;

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
                var response = versionManager.Version(command);
                if (response.Items.Any())
                {
                    Console.WriteLine("Updated the following versions:");

                    foreach (var item in response.Items)
                    {
                        if (!string.IsNullOrEmpty(item.ErrorMessage))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{item.CsprojFile}: {item.ErrorMessage}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"{item.CsprojFile}: {item.OldVersion} -> {item.NewVersion}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No *.csproj files found...");
                }
            }
            catch (ArgumentException aex)
            {
                Console.WriteLine($"Error: {aex.Message}");
                return;
            }

        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"Usage: dotnet-version [<version> | major | minor | patch]");
        }
    }
}
