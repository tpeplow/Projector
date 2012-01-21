using Projector.Model;

namespace Projector.Parser
{
    public interface IFileParser
    {
        void Parse(string file, CodeDirectory codeDirectory);
    }
}