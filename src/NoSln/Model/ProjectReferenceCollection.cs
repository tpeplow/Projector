using System.Collections;
using System.Collections.Generic;
using NoSln.Collections;

namespace NoSln.Model
{
    public class ProjectReferenceCollection : IEnumerable<ProjectReference>
    {
        readonly IDictionary<string, ProjectReference> references = new Dictionary<string, ProjectReference>();

        public ProjectReferenceCollection(IEnumerable<ProjectReference> references)
        {
            references.Each(Add);
        }

        public void Add(ProjectReference projectReference)
        {
            references.Add(projectReference.Name, projectReference);
        }

        public bool Contains(string referenceName)
        {
            return references.ContainsKey(referenceName);
        }

        public ProjectReference this[string referenceName]
        {
            get { return references[referenceName]; }
        }

        public IEnumerator<ProjectReference> GetEnumerator()
        {
            return references.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}