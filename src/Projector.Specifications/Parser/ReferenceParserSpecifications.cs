using System;
using System.Linq;
using Machine.Specifications;
using Projector.Model;
using Projector.Parser;

namespace Projector.Specifications.Parser
{
    [Subject(typeof(ReferenceParser))]
    public class when_parsing_references
    {
        static ReferenceParser referenceParser;
        static ReferenceCollection referenceCollection;
        static CodeDirectory codeDirectory;
        static int expectedReferences = 5;

        Establish context = () => 
                                {
                                    referenceParser = new ReferenceParser();
                                };

        Because of = () =>
                         {
                             codeDirectory = new CodeDirectory("test", "test");
                             referenceParser.Parse(
                                 "System" + Environment.NewLine
                                 + "    " + Environment.NewLine // ignored
                                 + "System.IO" + Environment.NewLine
                                 + " # Ignored" + Environment.NewLine // ignored
                                 + " ShouldBeTrimmed " + Environment.NewLine
                                 + "AReference ..\\Libs\\Some framework\\AReference.dll" + Environment.NewLine
                                 + "BReference ..\\libs\\SomethingVersioned.1.0\\SomethingVersioned.1.0.dll", codeDirectory);
                             referenceCollection = codeDirectory.References;
                         };

        It should_add_simple_references_such_as_system = () => referenceCollection.Contains("System").ShouldBeTrue();

        It should_should_contain_simple_references_such_as_system_io = () => referenceCollection.Contains("System").ShouldBeTrue();

        It should_ignore_empty_lines = () => referenceCollection.Count().ShouldEqual(expectedReferences);

        It should_ignore_lines_starting_with_hash = () => referenceCollection.Count().ShouldEqual(expectedReferences);

        It should_should_trim_whitespace = () => referenceCollection.Contains("ShouldBeTrimmed").ShouldBeTrue();

        It should_include_hint_path_when_given = () => referenceCollection["AReference"].HintPath.ShouldEqual("..\\Libs\\Some framework\\AReference.dll");

        It should_include_hint_paths_with_numbers = () => referenceCollection["BReference"].HintPath.ShouldEqual("..\\libs\\SomethingVersioned.1.0\\SomethingVersioned.1.0.dll");
    }
}