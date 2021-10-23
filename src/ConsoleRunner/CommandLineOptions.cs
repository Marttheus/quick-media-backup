using CommandLine;
using Media.Backup.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRunner
{
    public class CommandLineOptions
    {
        [Option('t', "file_types", Separator = ',', Required = true, HelpText = "Select the desired file types.")]
        public IList<FileType> FileTypes { get; set; }
        [Option('p', "path", Required = false, HelpText = "Path to backup.")]
        public string Path { get; set; } = Environment.CurrentDirectory;
        [Option('c', "read_path_children", Required = false, HelpText = "Read children paths.")]
        public bool ReadPathChildren { get; set; } = false;
        [Option('d', "files_destination", Required = true, HelpText = "Files destination.")]
        public string FilesDestination { get; set; }
    }
}
