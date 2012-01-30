using Machine.Specifications;
using Projector.Conventions.SuggestedStructure;

namespace Projector.Specifications.Conventions.SuggestedStructure
{
    [Subject(typeof(ProjectTypeNamingConvention))]
    public abstract class OutputTypeNamingConventionSpecificatoins
    {
        protected static string directoryName;
        protected static ProjectType result;
        static ProjectTypeNamingConvention projectTypeNamingConvention;

        Establish context = () =>
        {
            directoryName = string.Empty;
            projectTypeNamingConvention = new ProjectTypeNamingConvention();
        };

        Because of = () => result = projectTypeNamingConvention.GetProjectType(directoryName);
    }

    [Subject(typeof(ProjectTypeNamingConvention))]
    public class when_file_name_contains_console : OutputTypeNamingConventionSpecificatoins
    {
        Establish context = () => directoryName = "SomeNamespace.Something.Console";
        It should_set_output_type_to_exe = () => result.OutputType.ShouldEqual("Exe");
    }

    [Subject(typeof(ProjectTypeNamingConvention))]
    public class when_file_name_contains_exe : OutputTypeNamingConventionSpecificatoins
    {
        Establish context = () => directoryName = "SomeNamespace.Something.Exe";
        It should_set_output_type_to_exe = () => result.OutputType.ShouldEqual("Exe");
    }

    [Subject(typeof(ProjectTypeNamingConvention))]
    public class when_file_name_contains_no_contextual_information : OutputTypeNamingConventionSpecificatoins
    {
        Establish context = () => directoryName = "SomeNamespace.Something";
        It should_set_output_type_to_library = () => result.OutputType.ShouldEqual("Library");
    }

    [Subject(typeof(ProjectTypeNamingConvention))]
    public class when_file_name_contains_web : OutputTypeNamingConventionSpecificatoins
    {
        Establish context = () => directoryName = "SomeNamespace.Something.SomeWebsite";
        It should_be_a_web_project = () => result.ShouldBeOfType<ProjectTypes.Web>();
        It should_set_output_type_to_library = () => result.OutputType.ShouldEqual("Library");
    }
}