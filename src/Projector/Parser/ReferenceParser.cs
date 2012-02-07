using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Projector.Collections;
using Projector.Model;

namespace Projector.Parser
{
    public class ReferenceParser : FileParser<ReferenceCollection>
    {
        private static readonly Regex ReferenceLineExpression = new Regex(@"([a-z\._]+)(?: ([a-z\.\\ 0-9]+)){0,1}", RegexOptions.IgnoreCase);

        public override ReferenceCollection Parse(string file)
        {
            return new ReferenceCollection(Parse(file.GetLines()));
        }

        protected override void UpdateCodeDirectory(ReferenceCollection parsedFile, CodeDirectory codeDirectory)
        {
            parsedFile.Each(codeDirectory.References.Add);
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