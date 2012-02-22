using System.Text;
using ILC.Seve.Genetics;

namespace ILC.Seve.Web
{
    public class JSONWebSerializer : WebSerializer
    {
        public string Serialize(Individual individual)
        {
            var builder = new StringBuilder();

            builder.Append("[");

            foreach(var vertex in individual.Graph.Vertices)
            {
                builder.Append("[");
                builder.Append(vertex.ScaledX);
                builder.Append(",");
                builder.Append(vertex.ScaledY);
                builder.Append(",");
                builder.Append(vertex.ScaledZ);
                builder.Append("],");
            }

            builder.Remove(builder.Length - 1, 1); // Remove the trailing comma
            builder.Append("]");

            return builder.ToString();
        }
    }
}
