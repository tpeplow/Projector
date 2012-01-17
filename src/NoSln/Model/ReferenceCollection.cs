using System.Collections;
using System.Collections.Generic;
using NoSln.Collections;

namespace NoSln.Model
{
    public class ReferenceCollection : IEnumerable<AssemblyReference>
    {
        readonly IDictionary<string, AssemblyReference> references = new Dictionary<string, AssemblyReference>();

        public ReferenceCollection()
        {
        }

        public ReferenceCollection(IEnumerable<AssemblyReference> references)
        {
            references.Each(Add);
        }

        public void Add(AssemblyReference assemblyReference)
        {
            references.Add(assemblyReference.Name, assemblyReference);
        }

        public bool Contains(string referenceName)
        {
            return references.ContainsKey(referenceName);
        }

        public AssemblyReference this[string referenceName]
        {
            get { return references[referenceName]; }
        }

        public IEnumerator<AssemblyReference> GetEnumerator()
        {
            return references.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}