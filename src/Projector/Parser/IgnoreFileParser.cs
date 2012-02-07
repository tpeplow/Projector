using System.Collections.Generic;
using Projector.Model;

namespace Projector.Parser
{
    public class IgnoreFileParser : FileParser<FileInclusionPolicy>
    {
        public override FileInclusionPolicy Parse(string ignoreFile)
        {
            return Parse(ignoreFile.GetLines());
        }

        FileInclusionPolicy Parse(IEnumerable<string> lines)
        {
            var fileInclusionPolicy = new FileInclusionPolicy();
            foreach (var line in lines.SkipEmptyOrCommentedLines())
            {
                if (line.StartsWith("^"))
                    fileInclusionPolicy.AddInclude(line.Substring(1));
                else
                    fileInclusionPolicy.AddExclude(line);
            }

            return fileInclusionPolicy;
        }

        protected override void UpdateCodeDirectory(FileInclusionPolicy parsedFile, CodeDirectory codeDirectory)
        {
            codeDirectory.FileInclusionPolicy = parsedFile;
        }
    }
}