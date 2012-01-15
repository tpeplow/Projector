using System.Collections.Generic;
using NoSln.Model;

namespace NoSln.Parser
{
    public class IgnoreFileParser
    {
        public FileInclusionPolicy Parse(string ignoreFile)
        {
            return Parse(ignoreFile.GetLines());
        }

        public FileInclusionPolicy Parse(IEnumerable<string> lines)
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
    }
}