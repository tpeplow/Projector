namespace NoSln.IO
{
    public interface IFile
    {
        string FilePath { get; }
        string Contents { get; }
    }
}