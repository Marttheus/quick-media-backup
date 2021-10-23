using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Media.Backup.IO
{
    public static class Manager
    {
        public static string[] GetFiles(string path, bool readPathChildren, IList<FileType> fileTypes)
        {
            SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (readPathChildren)
                searchOption = SearchOption.AllDirectories;

            Expression<Func<string, bool>> predicate = x => false;

            if (fileTypes.Any(_ => _ == FileType.IMAGE))
            {
                var compiled = predicate.Compile();
                predicate = x => compiled(x) ||
                                 x.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".gif", StringComparison.OrdinalIgnoreCase);
            }

            if (fileTypes.Any(_ => _ == FileType.AUDIO))
            {
                var compiled = predicate.Compile();
                predicate = x => compiled(x) ||
                                 x.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase);
            }

            if (fileTypes.Any(_ => _ == FileType.VIDEO))
            {
                var compiled = predicate.Compile();
                predicate = x => compiled(x) ||
                                 x.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".wmv", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".avi", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".mov", StringComparison.OrdinalIgnoreCase);
            }

            if (fileTypes.Any(_ => _ == FileType.DOCUMENT))
            {
                var compiled = predicate.Compile();
                predicate = x => compiled(x) ||
                                 x.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                                 x.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase);
            }

            return Directory.GetFiles(path, "*", searchOption).Where(predicate.Compile()).ToArray();
        }
    }
}
