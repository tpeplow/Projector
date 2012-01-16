using NoSln.Model;

namespace NoSln.Parser
{
    public interface IFileParser
    {
        void Parse(string file, CodeDirectory codeDirectory);
    }
}