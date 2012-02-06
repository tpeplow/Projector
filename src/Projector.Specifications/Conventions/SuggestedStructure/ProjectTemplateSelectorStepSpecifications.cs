using System.Collections.Generic;
using System.Xml.Linq;
using Auto.Moq;
using Machine.Specifications;
using Projector.Conventions.SuggestedStructure;
using Projector.Model;
using Projector.Model.Output;
using Projector.Specifications.IO;
using Projector.Specifications.Model;

namespace Projector.Specifications.Conventions.SuggestedStructure
{
    [Subject(typeof(ProjectTemplateSelectorStep))]
    public class when_project_has_no_template : ProjectTemplateSelectorStepSpecifications
    {
        It should_pick_the_tempalte_for_the_project_type = () => Solution.GetProject("Test").ProjectTemplate.Xml.ShouldNotBeNull();
    }

    [Subject(typeof(ProjectTemplateSelectorStep))]
    public class when_project_already_has_a_template : ProjectTemplateSelectorStepSpecifications
    {
        static XDocument someXml = XDocument.Parse("<hello />");
        Establish context = () => Solution.GetProject("Test").ProjectTemplate = new ProjectTemplate { Xml = someXml } ;
        It should_pick_the_tempalte_for_the_project_type = () => Solution.GetProject("Test").ProjectTemplate.Xml.ShouldEqual(someXml);
    }

    [Subject(typeof(ProjectTemplateSelectorStep))]
    public abstract class ProjectTemplateSelectorStepSpecifications
    {
        static AutoMoq<ProjectTemplateSelectorStep> projectTemplateSelectorStep;
        protected static Solution Solution;
        protected static CodeDirectory CodeDirectory;
        Establish context = () =>
        {
            InitDirectory();
            projectTemplateSelectorStep = new AutoMoq<ProjectTemplateSelectorStep>();
            projectTemplateSelectorStep.GetMock<IProjectTypeNamingConvention>().Setup(x => x.GetProjectType("Test")).Returns(new TestProjectType());
        };

        static void InitDirectory()
        {
            Solution = new Solution();
            CodeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            var tempaltes = TestEntityFactory.CreateCodeDirectory("_templates");
            tempaltes.AddFile(new TestFile("Test") {Contents = "<root />"});
            CodeDirectory.AddCodeDirectory(tempaltes);
            Solution.AddProject(new Project {AssemblyName = "Test", Name = "Test"});
        }

        Because of = () => projectTemplateSelectorStep.Object.Execute(Solution, CodeDirectory);

        private class TestProjectType : ProjectType
        {
            public override string OutputType
            {
                get { return "Test"; }
            }

            public override string Name
            {
                get { return "Test"; }
            }

            public override IEnumerable<string> NamingConventions
            {
                get { yield return "Test"; }
            }
        }
    }
}