using System.Text;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Web
{
    /// <summary>
    /// A serializer that transforms individuals into JSON strings.  Each string
    /// is just a list of all the points.
    /// </summary>
    /// <example>
    /// The following string represents an individual with 3 points (1, 2, 3),
    /// (4, 5, 6), and (7, 8, 9).
    /// <code>
    /// [[1, 2, 3], [4, 5, 6], [7, 8, 9]]
    /// </code>
    /// </example>
    public class JSONWebSerializer : WebSerializer
    {
        public string Serialize(VertexGraph graph)
        {
            var builder = new StringBuilder();

            builder.Append("[");

            foreach(var vertex in graph.Vertices)
            {
                builder.AppendFormat("[{0},{1},{2}],", vertex.ScaledX, vertex.ScaledY, vertex.ScaledZ);
            }

            builder.Remove(builder.Length - 1, 1); // Remove the trailing comma
            builder.Append("]");

            return builder.ToString();
        }
    }
}
