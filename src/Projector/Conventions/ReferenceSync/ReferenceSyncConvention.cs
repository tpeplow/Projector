using System;
using System.Linq;
using System.Xml.Linq;
using Projector.Collections;
using Projector.IO;
using Projector.Model;
using Projector.Parser;
using Projector.Serializers;

namespace Projector.Conventions.ReferenceSync
{
    public class ReferenceSyncConvention : IModifyFileSystemConvention
    {
        readonly IProjectorSerializer<ReferenceCollection> referenceSerializer;
        readonly IFileParser<ReferenceCollection> referenceParser;

        public ReferenceSyncConvention(IProjectorSerializer<ReferenceCollection> referenceSerializer, IFileParser<ReferenceCollection> referenceParser)
        {
            this.referenceSerializer = referenceSerializer;
            this.referenceParser = referenceParser;
        }

        public void Update(IDirectory directory)
        {
            var projectFile = GetProjectFile(directory);
            if (projectFile != null)
            {
                var references = GetReferencesFile(directory);

                AddMissingReferencesToReferenceCollection(projectFile, references);

                var referenceFile = referenceSerializer.Serialize(references);

                directory.WriteFile(ParserRegistry.ReferencesFileName, referenceFile);
            }
            directory.Directories.Each(Update);
        }

        static void AddMissingReferencesToReferenceCollection(XDocument projectFile, ReferenceCollection references)
        {
            var referencesInProject = from project in projectFile.Elements()
                                      from itemGroup in project.Elements(Msbuild.DefaultNamespace + "ItemGroup")
                                      from refElement in itemGroup.Elements()
                                      where refElement.Name == Msbuild.DefaultNamespace + "Reference"
                                            || refElement.Name == Msbuild.DefaultNamespace + "ProjectReference"
                                      select CreateReference(refElement);

            var newReferences = referencesInProject.Except(references).ToArray();

            newReferences.Each(references.Add);
        }

        static ReferenceInformation CreateReference(XElement refElement)
        {
            string name;
            string hintPath = null;
            var nameElement = refElement.Element(Msbuild.DefaultNamespace + "Name");
            if (nameElement == null)
            {
                var refAttribute = refElement.Attribute("Include");
                name = refAttribute == null ? string.Empty : refAttribute.Value;
            }
            else
            {
                name = nameElement.Value;
            }

            var hintPathElement = refElement.Element(Msbuild.DefaultNamespace + "HintPath");
            if (hintPathElement != null)
            {
                hintPath = hintPathElement.Value;
            }

            return new ReferenceInformation(name, hintPath);
        }

        ReferenceCollection GetReferencesFile(IDirectory directory)
        {
            var references = directory.Files
                .Where(x => x.FileName.Equals(ParserRegistry.ReferencesFileName, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Contents)
                .FirstOrDefault();

            return references == null ? new ReferenceCollection() : referenceParser.Parse(references);
        }

        static XDocument GetProjectFile(IDirectory directory)
        {
            var projectFile = directory.Files.FirstOrDefault(x => x.FileName.EndsWith(".csproj", StringComparison.InvariantCultureIgnoreCase));
            return projectFile == null ? null : XDocument.Parse(projectFile.Contents);
        }
    }
}