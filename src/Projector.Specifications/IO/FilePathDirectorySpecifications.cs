using Machine.Specifications;
using Projector.IO;

namespace Projector.Specifications.IO
{
    [Subject(typeof(FilePathDictionary<string>))]
    public class when_
    {
        static FilePathDictionary<string> filePathDictionary;
        Establish context = () => 
                                {
                                    filePathDictionary = new FilePathDictionary<string>();
                                };

        Because of = () => { };

        It should_doSomethingSpaced = () => { };
    }
}