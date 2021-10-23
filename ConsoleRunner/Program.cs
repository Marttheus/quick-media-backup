using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Media.Backup.IO;
using Yaap;
using System.IO;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                   .WithParsed<CommandLineOptions>(options =>
                   {
                       Run(options);
                   })
                   .WithNotParsed<CommandLineOptions>(errors =>
                   {
                       HandleParseError(errors);
                   });
        }

        static void Run(CommandLineOptions options)
        {
            var files = Manager.GetFiles(options.Path, options.ReadPathChildren, options.FileTypes);

            Directory.CreateDirectory(options.FilesDestination);

            foreach(var file in files.Yaap())
            {
                File.Copy(file, Path.Combine(options.FilesDestination, Path.GetFileName(file)), true);
            }
        }

        static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = -2;
            Console.WriteLine("Errors {0}", errs.Count());
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            Console.WriteLine("Exit code {0}", result);
            return result;
        }
    }
}
