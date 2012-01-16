using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NoSln.Model;

namespace NoSln.Parser
{
    public class ReferenceParser : IFileParser
    {
        private static readonly Regex ReferenceLineExpression = new Regex(@"([a-z\._]+)(?: ([a-z\.\\ ]+)){0,1}", RegexOptions.IgnoreCase);

        public ReferenceCollection Parse(string file)
        {
            return Parse(file.GetLines());
        }

        ReferenceCollection Parse(IEnumerable<string> lines)
        {
            return new ReferenceCollection(lines.SkipEmptyOrCommentedLines()
                                                       .Select(x => ReferenceLineExpression.Match(x))
                                                       .Select(CreateReference));
        }

        static AssemblyReference CreateReference(Match match)
        {
            return new AssemblyReference(match.Groups[1].Value, match.Groups[2].Value);
        }

        void IFileParser.Parse(string file, CodeDirectory codeDirectory)
        {
            codeDirectory.References = Parse(file);
        }
    }
}