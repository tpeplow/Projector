using System.Linq;
using Machine.Specifications;
using Projector.Model;
using Projector.Serializers;

namespace Projector.Specifications.Serializers
{
    [Subject(typeof(ReferenceSerializer))]
    public abstract class ReferenceSerializerSpecifications
    {
        protected static ReferenceCollection References;
        protected static string Result;
        static ReferenceSerializer referenceSerializer;

        Establish context = () => 
        {
            References = new ReferenceCollection();
            referenceSerializer = new ReferenceSerializer();
        };

        Because of = () => Result = referenceSerializer.Serialize(References);
    }

    [Subject(typeof(ReferenceSerializer))]
    public class when_serializing : ReferenceSerializerSpecifications
    {
        Establish context = () =>
        {
            References.Add(new ReferenceInformation("System"));
            References.Add(new ReferenceInformation("Another", "some hint path"));
        };

        It should_write_reference_name = () => Result.GetLines().First().ShouldEqual("System");
        It should_write_hint_path = () => Result.GetLines().Last().ShouldEqual("Another some hint path");
    }
}