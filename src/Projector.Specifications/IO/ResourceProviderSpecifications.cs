using Machine.Specifications;
using Projector.Conventions.SuggestedStructure;
using Projector.IO;

namespace Projector.Specifications.IO
{
    [Subject(typeof(ResourceProvider))]
    public abstract class when_providing_resource
    {
        protected static ResourceProvider resourceProvider;
        protected static string result;

        Establish context = () =>
        {
            resourceProvider = new ResourceProvider();
        };
    }

    [Subject(typeof(ResourceProvider))]
    public class when_resource_exists : when_providing_resource
    {
        Because of = () => result = resourceProvider.ReadResource<when_providing_resource>("TestResource.txt");

        It should_read_resource_from_dll = () => result.ShouldEqual("this is a test resource");
    }

    [Subject(typeof(ResourceProvider))]
    public class when_does_not_resource_exists : when_providing_resource
    {
        Because of = () => result = resourceProvider.ReadResource<when_providing_resource>("not at home");

        It should_read_resource_from_dll = () => result.ShouldEqual(string.Empty);
    }

    [Subject(typeof(ResourceProvider))]
    public class when_resource_exists_a_sub_namespace : when_providing_resource
    {
        Because of = () => result = resourceProvider.ReadResource<GitIgnoreGenerator>("Resources.GitIgnore.txt");

        It should_read_resource_from_dll = () => result.ShouldNotEqual(string.Empty);
    }
}