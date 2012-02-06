using System;
using System.Collections.Generic;

namespace Projector.Parser
{
    public interface IParserRegistry
    {
        IFileParser GetParserForFile(string fileName);
    }

    public class ParserRegistry : IParserRegistry
    {
        public const string ProjectFileName = "proj.nosln";
        public const string ReferencesFileName = "references.nosln";
        public const string IgnoreFileName = "ignore.nosln";
        public const string TemplateFileName = "template.nosln";
        public const string FileTypesFileName = "filetypes.nosln";

        static readonly Dictionary<string, IFileParser> Parsers = new Dictionary<string, IFileParser>(StringComparer.InvariantCultureIgnoreCase)
        {
            { ProjectFileName, new ProjectParser(new GuidGenerator()) },
            { ReferencesFileName, new ReferenceParser() },
            { IgnoreFileName, new IgnoreFileParser() },
            { TemplateFileName, new ProjectTemplateParser() },
            { FileTypesFileName, new FileTypeParser() }
        };

        public IFileParser GetParserForFile(string fileName)
        {
            IFileParser parser;
            Parsers.TryGetValue(fileName.ToLower(), out parser);
            return parser;
        }
    }
}