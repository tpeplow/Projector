namespace NoSln.OutputPipeline.OutputGenerator
{
    public interface IOutputWriterResolver
    {
        IOutputXmlWriter Resolve<TPartType>();
    }
}