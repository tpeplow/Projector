using Projector.Model;

namespace Projector.Parser
{
    public class ProjectTemplateParser : IFileParser
    {
        public void Parse(string file, CodeDirectory codeDirectory)
        {
            codeDirectory.ProjectTemplate = file;
        }
    }
}