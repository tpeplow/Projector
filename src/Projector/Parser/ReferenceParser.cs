using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Projector.Collections;
using Projector.Model;

namespace Projector.Parser
{
    public class ReferenceParser : IFileParser
    {
        private static readonly Regex ReferenceLineExpression = new Regex(@"([a-z\._]+)(?: ([a-z\.\\ 0-9]+)){0,1}", RegexOptions.IgnoreCase);

        public void Parse(string file, CodeDirectory codeDirectory)
        {
            var references = Parse(file);
            references.Each(codeDirectory.References.Add);
        }

        IEnumerable<ReferenceInformation> Parse(string file)
        {
            return Parse(file.GetLines());
        }

        IEnumerable<ReferenceInformation> Parse(IEnumerable<string> lines)
        {
            return lines.SkipEmptyOrCommentedLines()
                .Select(x => ReferenceLineExpression.Match(x))
                .Select(CreateReference);
        }

        static ReferenceInformation CreateReference(Match match)
        {
            return new ReferenceInformation(match.Groups[1].Value, match.Groups[2].Value);
        }
    }
}