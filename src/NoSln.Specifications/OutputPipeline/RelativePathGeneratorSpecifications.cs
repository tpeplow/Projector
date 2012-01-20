using Machine.Specifications;
using NoSln.OutputPipeline;

namespace NoSln.Specifications.OutputPipeline
{
    [Subject(typeof(RelativePathGenerator))]
    public class when_generating_a_relative_path
    {
        static RelativePathGenerator relativePathGenerator;
        Establish context = () => 
                                {
                                    relativePathGenerator = new RelativePathGenerator();
                                };

        It should_shorten_paths_when_parent_is_above 
            = () => relativePathGenerator.GeneratePath("c:\\test\\", "c:\\test\\a sub folder\\afile.txt").ShouldEqual("a sub folder\\afile.txt");


        It should_shorten_paths_to_file_when_parent_is_above
            = () => relativePathGenerator.GeneratePath("c:\\test\\another project\\", "c:\\test\\a project\\someproj.csproj").ShouldEqual("..\\a project\\someproj.csproj");

        It should_create_correct_path_where_relative_path_is_below_parent 
            = () => relativePathGenerator.GeneratePath("c:\\test\\a sub folder\\", "c:\\test").ShouldEqual("..\\..\\test");
        
        It should_create_correct_path_when_relative_too_branches_off_from_the_parent 
            = () => relativePathGenerator.GeneratePath("c:\\test\\some project\\", "c:\\test\\assemblies\\something.dll").ShouldEqual("..\\assemblies\\something.dll");
    }
}