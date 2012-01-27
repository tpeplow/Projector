using System;
using System.Collections.Generic;
using Projector.IO;
using Projector.Model;

namespace Projector.Parser
{
    public interface ISolutionBuilder
    {
        CodeDirectory BuildFromDirectory(IDirectory filePath);
    }

    public class SolutionBuilder : ISolutionBuilder
    {
        readonly IParserRegistry parserRegistry;

        public SolutionBuilder(IParserRegistry parserRegistry)
        {
            if (parserRegistry == null) throw new ArgumentNullException("parserRegistry");
            this.parserRegistry = parserRegistry;
        }

        public CodeDirectory BuildFromDirectory(IDirectory directory)
        {
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