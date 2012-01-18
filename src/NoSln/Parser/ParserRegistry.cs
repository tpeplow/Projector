using System;
using System.Collections.Generic;

namespace NoSln.Parser
{
    public interface IParserRegistry
    {
        IFileParser GetParserForFile(string fileName);
    }

    public class ParserRegistry : IParserRegistry
    {
        static readonly Dictionary<string, IFileParser> Parsers = new Dictionary<string, IFileParser>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "proj.nosln", new ProjectParser(new GuidGenerator()) },
            { "references.nosln", new ReferenceParser() },
            { "ignore.nosln", new IgnoreFileParser() },
            { "template.nosln", new ProjectTemplateParser() }
        };

        public IFileParser GetParserForFile(string fileName)
        {
            IFileParser parser;
            Parsers.TryGetValue(fileName.ToLower(), out parser);
            return parser;
        }
    }
}