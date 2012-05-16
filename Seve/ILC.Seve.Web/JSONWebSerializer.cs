using System.Collections.Generic;
using System.Linq;
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
        private List<double> Averages = new List<double>();

        public string Rewind()
        {
            var averages = string.Join(",", Averages);

            return string.Format("{{ \"average_fitnesses\": [{0}] }}", averages);
        }

        public string Serialize(List<Individual> state)
        {
            var average = state.Select(a => a.Fitness).Average();
            Averages.Add(average);

            return string.Format("{{ \"average_fitness\": {0} }}", average);
        }
    }
}
