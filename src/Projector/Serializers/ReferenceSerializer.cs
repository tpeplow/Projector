using System.Text;
using Projector.Model;

namespace Projector.Serializers
{
    public class ReferenceSerializer : IProjectorSerializer<ReferenceCollection>
    {
        public string Serialize(ReferenceCollection references)
        {
            var stringBuilder = new StringBuilder();

            foreach (var reference in references)
            {
                stringBuilder.AppendLine(string.Format("{0} {1}", reference.Name, reference.HintPath).Trim());
            }

            return stringBuilder.ToString();
        }
    }
}