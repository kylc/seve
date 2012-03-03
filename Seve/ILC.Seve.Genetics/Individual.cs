using System;
using ILC.Seve.Graph;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// Represents a single individual in the Genetic Algorithm simulation.
    /// This class is to have unique implementations depending on the problem
    /// at hand.
    /// </summary>
    public abstract class Individual : IComparable
    {
        public VertexGraph Graph { get; set; }
        public VertexGraph SimulatedGraph { get; set; }

        /// <summary>
        /// A cached fitness value.
        /// </summary>
        public int Fitness
        {
            get
            {
                if (!_fitness.HasValue)
                {
                    _fitness = CalculateFitness();
                }

                return _fitness.Value;
            }
        }

        private int? _fitness;

        /// <summary>
        /// Creates a random individual to be used in the initial population.
        /// </summary>
        public Individual()
        {
            Graph = new VertexGraph();
        }

        /// <summary>
        /// Assess the fitness of the individual.  This operation may take a
        /// while, so the value should be stored and accessed from the Fitness
        /// property.
        /// </summary>
        /// <returns>A score.  This does not ned to be anything specific, as
        /// long as a more fit individual always has a higher fitness than
        /// a less fit individual.</returns>
        public abstract int CalculateFitness();

        public int CompareTo(object obj)
        {
            var other = (Individual)obj;

            return Fitness.CompareTo(other.Fitness);
        }
    }
}
