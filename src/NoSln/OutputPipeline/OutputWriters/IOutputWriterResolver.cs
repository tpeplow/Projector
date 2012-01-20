namespace NoSln.OutputPipeline.OutputWriters
{
    public interface IOutputWriterResolver
    {
        IOutputXmlWriter Resolve<TPartType>();
    }
}