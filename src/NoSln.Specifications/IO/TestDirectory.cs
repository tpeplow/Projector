﻿using System.Collections.Generic;
using Projector.IO;

namespace Projector.Specifications.IO
{
    public class TestDirectory : IDirectory
    {
        public TestDirectory()
        {
            Directories = new List<IDirectory>();
            Files = new List<IFile>();
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public IEnumerable<IFile> Files { get; set; }

        public IEnumerable<IDirectory> Directories { get; set; }
    }
}