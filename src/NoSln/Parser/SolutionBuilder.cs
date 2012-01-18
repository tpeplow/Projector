using System;
using System.Collections.Generic;
using NoSln.IO;
using NoSln.Model;

namespace NoSln.Parser
{
    public interface ISolutionBuilder
    {
        CodeDirectory BuildFromPath(string filePath);
    }

    public class SolutionBuilder : ISolutionBuilder
    {
        readonly IFileSystem fileSystem;
        readonly IParserRegistry parserRegistry;

        public SolutionBuilder(IFileSystem fileSystem, IParserRegistry parserRegistry)
        {
            if (fileSystem == null) throw new ArgumentNullException("fileSystem");
            if (parserRegistry == null) throw new ArgumentNullException("parserRegistry");
            this.fileSystem = fileSystem;
            this.parserRegistry = parserRegistry;
        }

        public CodeDirectory BuildFromPath(string filePath)
        {
            var directory = fileSystem.GetDirectory(filePath);
            var codeDirectory = new CodeDirectory(directory.Name, directory.Path);

            BuildFromDirectory(directory, codeDirectory);

            return codeDirectory;
        }

        void BuildFromDirectory(IDirectory directory, CodeDirectory codeDirectory)
        {
            ProcessFiles(directory.Files, codeDirectory);
            foreach (var subDirectory in directory.Directories)
            {
                var subCodeDirectory = new CodeDirectory(subDirectory.Name, subDirectory.Path);
                codeDirectory.AddCodeDirectory(subCodeDirectory);
                BuildFromDirectory(subDirectory, subCodeDirectory);
            }
        }

        void ProcessFiles(IEnumerable<IFile> files, CodeDirectory codeDirectory)
        {
            foreach (var file in files)
            {
                var parser = parserRegistry.GetParserForFile(file.FileName);
                if (parser != null)
                {
                    parser.Parse(file.Contents, codeDirectory);
                }
                else
                {
                    codeDirectory.AddFile(file);
                }
            }
        }
    }
}