using System.Collections;
using System.Collections.Generic;

namespace NoSln.Model
{
    public class ReferenceCollection : IEnumerable<ReferenceInformation>
    {
        readonly IDictionary<string, ReferenceInformation> references = new Dictionary<string, ReferenceInformation>();

        public void Add(ReferenceInformation referenceInformation)
        {
            references.Add(referenceInformation.Name, referenceInformation);
        }

        public bool Contains(string referenceName)
        {
            return references.ContainsKey(referenceName);
        }

        public ReferenceInformation this[string referenceName]
        {
            get { return references[referenceName]; }
        }

        public IEnumerator<ReferenceInformation> GetEnumerator()
        {
            return references.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}