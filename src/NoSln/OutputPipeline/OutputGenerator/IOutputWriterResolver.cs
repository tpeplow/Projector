namespace NoSln.OutputPipeline.OutputGenerator
{
    public interface IOutputWriterResolver
    {
        IOutputWriter Resolve<TPartType>();
    }
}