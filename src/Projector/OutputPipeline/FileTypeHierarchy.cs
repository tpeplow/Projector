using System;
using System.Collections.Generic;
using System.Linq;
using Projector.Collections;
using Projector.IO;
using Projector.Model;

namespace Projector.OutputPipeline
{
    public interface IFileTypeHierarchy
    {
        FileType GetFileType(string relativePath);
    }

    public class FileTypeHierarchy : IFileTypeHierarchy
    {
        readonly FilePathDictionary<IEnumerable<FileType>> fileTypes;
        readonly IWildcardMatcher wildcardMatcher;

        public FileTypeHierarchy(FilePathDictionary<IEnumerable<FileType>> fileTypes, IWildcardMatcher wildcardMatcher)
        {
            this.fileTypes = fileTypes;
            this.wildcardMatcher = wildcardMatcher;
        }

        public FileType GetFileType(string relativePath)
        {
            return fileTypes.FindAllInPath(relativePath)
                .SelectMany(x => x)
                .FirstOrDefault(x => wildcardMatcher.IsMatch(relativePath, x.FileNameWildcard));
        }
    }
}