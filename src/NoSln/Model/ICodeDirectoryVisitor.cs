using NoSln.IO;

namespace NoSln.Model
{
    public interface ICodeDirectoryVisitor
    {
        void Visit(AssemblyReference reference);
        void Visit(IFile file);
        void Visit(ProjectInfo project);
    }
}