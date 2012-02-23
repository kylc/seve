using System.Text;
using ILC.Seve.Genetics;

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
        public string Serialize(Individual individual)
        {
            var builder = new StringBuilder();

            builder.Append("[");

            foreach(var vertex in individual.Graph.Vertices)
            {
                builder.AppendFormat("[{0},{1},{2}],", vertex.ScaledX, vertex.ScaledY, vertex.ScaledZ);
            }

            builder.Remove(builder.Length - 1, 1); // Remove the trailing comma
            builder.Append("]");

            return builder.ToString();
        }
    }
}
