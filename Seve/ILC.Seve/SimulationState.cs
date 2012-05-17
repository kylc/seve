using System.Collections.Generic;
using ILC.Seve.Genetics;

namespace ILC.Seve
{
    public class SimulationState
    {
        public List<Individual> Individuals { get; private set; }
        public long ProcessingTime { get; private set; }

        public SimulationState(List<Individual> individuals, long processingTime)
        {
            Individuals = individuals;
            ProcessingTime = processingTime;
        }
    }
}
