using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NoSln.Model;

namespace NoSln.Parser
{
    public class ReferenceParser
    {
        private static readonly Regex ReferenceLineExpression = new Regex(@"([a-z\._]+)(?: ([a-z\.\\ ]+)){0,1}", RegexOptions.IgnoreCase);

        public ProjectReferenceCollection Parse(string file)
        {
            return Parse(file.GetLines());
        }

        public ProjectReferenceCollection Parse(IEnumerable<string> lines)
        {
            return new ProjectReferenceCollection(lines.SkipEmptyOrCommentedLines()
                                                       .Select(x => ReferenceLineExpression.Match(x))
                                                       .Select(CreateReference));
        }

        static ProjectReference CreateReference(Match match)
        {
            return new ProjectReference(match.Groups[1].Value, match.Groups[2].Value);
        }
    }
}