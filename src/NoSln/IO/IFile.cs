namespace NoSln.IO
{
    public interface IFile
    {
        string FilePath { get; }
        string FileName { get; }
        string Contents { get; }
    }
}