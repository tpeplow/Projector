using System;
using System.Collections.Generic;
using NoSln.IO;

namespace NoSln.Model
{
    public class CodeDirectory
    {
        readonly IList<IFile> files;
        readonly IList<CodeDirectory> directories; 
        public CodeDirectory(string name, string path)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");

            Name = name;
            Path = path;
            files = new List<IFile>();
            directories = new List<CodeDirectory>();
            Files = files;
            Directories = directories;
        }

        public ProjectInfo Project { get; set; }
        public ProjectReferenceCollection References { get; set; }
        public FileInclusionPolicy FileInclusionPolicy { get; set; }
        public IEnumerable<IFile> Files { get; private set; }
        public IEnumerable<CodeDirectory> Directories { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }

        public void AddFile(IFile file)
        {
            if (file == null) throw new ArgumentNullException("file");
            files.Add(file);
        }

        public void AddCodeDirectory(CodeDirectory codeDirectory)
        {
            if (codeDirectory == null) throw new ArgumentNullException("codeDirectory");
            directories.Add(codeDirectory);
        }
    }
}