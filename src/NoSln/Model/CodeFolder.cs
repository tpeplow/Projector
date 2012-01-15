using System.Collections.Generic;
using NoSln.IO;

namespace NoSln.Model
{
    public class CodeFolder
    {
        public ProjectInfo Project { get; set; }
        public ProjectReferenceCollection References { get; set; }
        public FileInclusionPolicy FileInclusionPolicy { get; set; }
        public IEnumerable<IFile> Files { get; set; }
        public IEnumerable<CodeFolder> Folders { get; set; } 
    }
}