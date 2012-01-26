using System.Collections.Generic;
using Projector.Collections;
using Projector.IO;
using Projector.Model;

namespace Projector.OutputPipeline
{
    public interface IFileTypeHierarchyBuilder
    {
        IFileTypeHierarchy Generate(CodeDirectory codeDirectory);
    }

    public class FileTypeHierarchyBuilder : IFileTypeHierarchyBuilder
    {
        readonly IWildcardMatcher wildcardMatcher;

        public FileTypeHierarchyBuilder(IWildcardMatcher wildcardMatcher)
        {
            this.wildcardMatcher = wildcardMatcher;
        }

        public IFileTypeHierarchy Generate(CodeDirectory codeDirectory)
        {
            var filePathDictionary = new FilePathDictionary<IEnumerable<FileType>>();
            AddFileTypes(codeDirectory, filePathDictionary);

            return new FileTypeHierarchy(filePathDictionary, wildcardMatcher);
        }

        void AddFileTypes(CodeDirectory codeDirectory, FilePathDictionary<IEnumerable<FileType>> filePathDictionary)
        {
            if (codeDirectory.FileTypes != null)
            {
                filePathDictionary.Add(codeDirectory.Path, codeDirectory.FileTypes);
            }
            codeDirectory.Directories.Each(x => AddFileTypes(x, filePathDictionary));
        }
    }
}