using System;
using System.Collections.Generic;

namespace NoSln.Parser
{
    public class ParserRegistry
    {
        static readonly Dictionary<string, IFileParser> Parsers = new Dictionary<string, IFileParser>(StringComparer.InvariantCultureIgnoreCase)
                                                                      {
                                                                          { "proj.nosln", new ProjectParser(new GuidGenerator()) },
                                                                          { "references.nosln", new ReferenceParser() },
                                                                          { "ignore.nosln", new IgnoreFileParser() }
                                                                      };

        public IFileParser GetParserForFile(string fileName)
        {
            IFileParser parser;
            Parsers.TryGetValue(fileName.ToLower(), out parser);
            return parser;
        }
    }
}