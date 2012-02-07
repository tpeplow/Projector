using Projector.Model;

namespace Projector.Parser
{
    public interface IFileParser
    {
        void Parse(string file, CodeDirectory codeDirectory);
    }

    public interface IFileParser<out T> : IFileParser
    {
        T Parse(string file);
    }

    public abstract class FileParser<T> : IFileParser<T>
    {
        public abstract T Parse(string file);

        void IFileParser.Parse(string file, CodeDirectory codeDirectory)
        {
            var parsedFile = Parse(file);
            UpdateCodeDirectory(parsedFile, codeDirectory);
        }

        protected abstract void UpdateCodeDirectory(T parsedFile, CodeDirectory codeDirectory);
    }
}