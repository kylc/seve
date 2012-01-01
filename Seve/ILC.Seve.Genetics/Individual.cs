using System;
using ILC.Seve.Graph;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// Represents a single individual in the Genetic Algorithm simulation.
    /// This class is to have unique implementations depending on the
    /// problem at hand.
    /// </summary>
    public abstract class Individual : IComparable
    {
        public VertexGraph Graph { get; set; }
        public int Fitness { get; set; }

        /// <summary>
        /// Creates a random individual to be used in the initial population.
        /// </summary>
        public Individual()
        {
            Graph = new VertexGraph();
        }

        /// <summary>
        /// Assess the fitness of the individual.
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
