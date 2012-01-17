using System;
using Machine.Specifications;
using Moq;
using NoSln.Model;
using NoSln.Specifications.IO;
using It = Machine.Specifications.It;
using Arg = Moq.It;

namespace NoSln.Specifications.Model
{
    [Subject(typeof(CodeDirectory))]
    public class when_visiting_a_code_directory_with_a_project
    {
        static CodeDirectory codeDirectory;
        static Mock<ICodeDirectoryVisitor> visitor;
        static int visitCount;
        static int projectVisit;
        static int referenceVisit;
        static int fileVisit;
        static int subDirectoryFile;
        Establish context = () => 
                                {
                                    codeDirectory = new CodeDirectory("test", "c:\\test")
                                    {
                                        Project = new ProjectInfo("project", "output", "namespace", Guid.NewGuid())
                                    };
                                    var assemblyReference = new AssemblyReference("name");
                                    codeDirectory.References.Add(assemblyReference);
                                    var file = new TestFile();
                                    codeDirectory.AddFile(file);
                                    var childDirectory = new CodeDirectory("child", "child");
                                    var childFile = new TestFile();
                                    childDirectory.AddFile(childFile);
                                    codeDirectory.AddCodeDirectory(childDirectory);

                                    visitor = new Mock<ICodeDirectoryVisitor>();
                                    visitor.Setup(x => x.Visit(Arg.IsAny<ProjectInfo>())).Callback(() => projectVisit = visitCount++);
                                    visitor.Setup(x => x.Visit(assemblyReference)).Callback(() => referenceVisit = visitCount++);
                                    visitor.Setup(x => x.Visit(file)).Callback(() => fileVisit = visitCount++);
                                    visitor.Setup(x => x.Visit(childFile)).Callback(() => subDirectoryFile = visitCount++);
                                };

        Because of = () => codeDirectory.AcceptVisitor(visitor.Object);

        It should_visit_project_first = () => projectVisit.ShouldEqual(0);
        It should_visit_references_second = () => referenceVisit.ShouldEqual(1);
        It should_visit_files_third = () => fileVisit.ShouldEqual(2);
        It should_visit_child_directories_files_fourth = () => subDirectoryFile.ShouldEqual(3);
    }

    [Subject(typeof(CodeDirectory))]
    public class when_visiting_a_code_directory_without_a_project_file
    {
        static CodeDirectory codeDirectory;
        static Mock<ICodeDirectoryVisitor> visitor;
        static int visitCount;
        static int fileVisit;
        static int subDirectoryFile;

        Establish context = () =>
                                {
                                    codeDirectory = new CodeDirectory("test", "c:\\test");
                                    var file = new TestFile();
                                    codeDirectory.AddFile(file);
                                    var childDirectory = new CodeDirectory("child", "child");
                                    var childFile = new TestFile();
                                    childDirectory.AddFile(childFile);
                                    codeDirectory.AddCodeDirectory(childDirectory);

                                    visitor = new Mock<ICodeDirectoryVisitor>();
                                    visitor.Setup(x => x.Visit(file)).Callback(() => fileVisit = visitCount++);
                                    visitor.Setup(x => x.Visit(childFile)).Callback(() => subDirectoryFile = visitCount++);
                                };

        Because of = () => codeDirectory.AcceptVisitor(visitor.Object);

        It should_visit_files_first = () => fileVisit.ShouldEqual(0);
        It should_visit_sub_directory_files_second = () => subDirectoryFile.ShouldEqual(1);
    }
}