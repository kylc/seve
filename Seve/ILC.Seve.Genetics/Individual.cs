using System;
using ILC.Seve.Graph;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// Represents a single individual in the Genetic Algorithm simulation.
    /// This class is to have unique implementations depending on the
    /// problem at hand.
    /// </summary>
    public abstract class Individual : IBinaryRepresentable, IComparable
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
        /// Cross the "genes" of two parents, creating a child.
        /// </summary>
        /// <param name="b">The second individual</param>
        /// <returns>A new individual composed of both parent's genes</returns>
        public Individual Cross(Individual b)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutate the given individual randomly.  Note that this method will
        /// only be called for a small subset of the population, so it should
        /// always mutate the individual it is given.
        /// </summary>
        /// <returns>The mutated individual</returns>
        public Individual Mutate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Assess the fitness of the individual.
        /// </summary>
        /// <returns>A score.  This does not ned to be anything specific, as
        /// long as a more fit individual always has a higher fitness than
        /// a less fit individual.</returns>
        public abstract int CalculateFitness(VertexGraph graph);

        public abstract byte[] ToBinary();

        public int CompareTo(object obj)
        {
            var other = (Individual)obj;

            return Fitness.CompareTo(other.Fitness);
        }
    }
}
