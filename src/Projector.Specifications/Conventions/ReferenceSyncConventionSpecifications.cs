using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Projector.Conventions.ReferenceSync;
using Projector.IO;
using Projector.Model;
using Projector.Parser;
using Projector.Serializers;
using Projector.Specifications.IO;
using Arg = Moq.It;

namespace Projector.Specifications.Conventions
{
    [Subject(typeof (ReferenceSyncConvention))]
    public class when_there_is_currently_no_references_file : when_there_are_references
    {
        It should_create_references_file = () => ProjectDirectory.Files.Select(x => x.FileName).ShouldContain(ParserRegistry.ReferencesFileName);
        It should_write_file_contents = () => ProjectDirectory.Files.First(x => x.FileName == ParserRegistry.ReferencesFileName).Contents.ShouldEqual("the file");
        It should_contain_all_references = () => UpdatedReferences.Count().ShouldEqual(3);
        It should_map_reference_names = () => UpdatedReferences.Select(x => x.Name).ShouldContainOnly("Auto.Moq", "System", "Projector");
        It should_map_hint_paths = () => UpdatedReferences.Select(x => x.HintPath).Where(x => x != null).ShouldContainOnly(@"..\..\lib\Auto.Moq.dll");
    }

    [Subject(typeof (ReferenceSyncConvention))]
    public class when_there_is_currently_a_references_file : when_there_are_references
    {
        Establish context = () => CurrentReferences = new ReferenceCollection(new[] { new ReferenceInformation("System"), });
        It should_merge_the_references = () => UpdatedReferences.Select(x => x.Name).ShouldContainOnly("Auto.Moq", "System", "Projector");
    }

    [Subject(typeof (ReferenceSyncConvention))]
    public class when_there_are_references : when_there_is_a_project_file
    {
        Establish context = () => ProjectFile.Contents = 
            @"<Project ToolsVersion='4.0' DefaultTargets='Build' xmlns='http://schemas.microsoft.com/developer/msbuild/2003'>
              <ItemGroup>
                <Reference Include='Auto.Moq'>
                  <HintPath>..\..\lib\Auto.Moq.dll</HintPath>
                </Reference>
                <Reference Include='System' />
              </ItemGroup>
              <ItemGroup>
                <ProjectReference Include='..\Projector\Projector.csproj'>
                  <Project>{5F221CC0-0187-4FE4-AB18-5E310CC5F106}</Project>
                  <Name>Projector</Name>
                </ProjectReference>
              </ItemGroup>
              </Project>";
    }

    [Subject(typeof(ReferenceSyncConvention))]
    public class when_there_is_a_project_file : ReferenceSyncConventionSpecifications
    {
        protected static TestFile ProjectFile;
        protected static IDirectory ProjectDirectory;

        Establish content = () =>
        {
            ProjectDirectory = Directory.CreateChildDirectory("AProject");
            ProjectFile = new TestFile("AProject.csproj");
            ((TestDirectory)ProjectDirectory).Files = new IFile[] {ProjectFile,};
        };
    }

    [Subject(typeof(ReferenceSyncConvention))]
    public abstract class ReferenceSyncConventionSpecifications
    {
        protected static IDirectory Directory;
        protected static ReferenceCollection CurrentReferences;
        protected static ReferenceCollection UpdatedReferences;
        static AutoMoq<ReferenceSyncConvention> referenceSyncConvention;

        Establish context = () =>
        {
            Directory = new TestDirectory();
            referenceSyncConvention = new AutoMoq<ReferenceSyncConvention>();
            referenceSyncConvention.GetMock<IFileParser<ReferenceCollection>>().Setup(x => x.Parse(Arg.IsAny<string>())).Returns(() => CurrentReferences);
            referenceSyncConvention.GetMock<IProjectorSerializer<ReferenceCollection>>()
                .Setup(x => x.Serialize(Arg.IsAny<ReferenceCollection>()))
                .Returns("the file")
                .Callback<ReferenceCollection>(c => UpdatedReferences = c);
        };

        Because of = () => referenceSyncConvention.Object.Update(Directory);
    }
}