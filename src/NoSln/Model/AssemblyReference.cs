using System;

namespace NoSln.Model
{
    public class AssemblyReference
    {
        public AssemblyReference(string name, string hintPath = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            Name = name;
            HintPath = hintPath;
        }

        public string Name { get; private set; }

        public string HintPath { get; private set; }
    }
}