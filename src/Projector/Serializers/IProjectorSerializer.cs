namespace Projector.Serializers
{
    public interface IProjectorSerializer<in T>
    {
        string Serialize(T objectToSerialize);
    }
}