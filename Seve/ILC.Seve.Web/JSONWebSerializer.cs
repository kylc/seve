using System.Collections.Generic;
using System.Linq;

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
        private List<SimulationState> States = new List<SimulationState>();

        public string Rewind()
        {
            var averages = string.Join(",", States.Select(s => s.Individuals.Average(x => x.Fitness)));
            var times = string.Join(",", States.Select(s => s.ProcessingTime));

            return string.Format("{{ \"average_fitnesses\": [{0}], processing_times: [{1}] }}", averages, times);
        }

        public string Serialize(SimulationState state)
        {
            States.Add(state);

            var average = state.Individuals.Select(a => a.Fitness).Average();

            return string.Format("{{ \"average_fitness\": {0}, \"processing_time\": {1} }}", average, state.ProcessingTime);
        }
    }
}
