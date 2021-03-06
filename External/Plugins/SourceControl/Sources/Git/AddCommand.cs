﻿using System.IO;

namespace SourceControl.Sources.Git
{
    class AddCommand : BaseCommand
    {
        public AddCommand(string path)
        {
            var args = $"add \"{Path.GetFileName(path)}\"";
            Run(args, Path.GetDirectoryName(path));
        }
    }
}
