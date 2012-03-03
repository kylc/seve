using System;
using System.Collections.Generic;
using System.Linq;
using ILC.Seve.Util;

namespace ILC.Seve.Genetics
{
    /// <summary>
    /// This class is responsible for performing all operations of the Genetic
    /// Algorithm.  It takes a bunch of individuals as an input (along with
    /// their fitness values) and allows the caller to step from one generation
    /// to the next.
    /// </summary>
    public class Algorithm
    {
        private readonly int PopulationSize;

        public List<Individual> Population;
        private BinarySerializer Constructor;
        private ICrossFunction CrossFunction;
        private IMutateFunction MutateFunction;

        private static Random Random;

        public Algorithm(int populationSize, BinarySerializer constructor, ICrossFunction crossFunction, IMutateFunction mutateFunction)
        {
            PopulationSize = populationSize;
            Constructor = constructor;
            CrossFunction = crossFunction;
            MutateFunction = mutateFunction;

            Random = new Random();

            MakeInitialPopulation();
        }

        /// <summary>
        /// Progress the population to the next generation.  This method
        /// handles all crossing and mutating.
        /// </summary>
        public void Step()
        {
            // Fitness is assigned in Simulation class

            var targetSize = Population.Count;

            // Sort the population by the calculated fitnesses
            // Select and breed the top portion of the population
            Population = Population
                .OrderByDescending(individual => individual.Fitness)
                .Take(Population.Count / 2)
                .OrderBy(a => Guid.NewGuid()).ToList();

            var newPopulation = new List<Individual>(Population.Count);

            while(newPopulation.Count < targetSize)
            {
                var father = Population[Random.Next(Population.Count)];
                var mother = (Individual) null;
                do
                {
                    mother = Population[Random.Next(Population.Count)];
                } while (father == mother || mother == null);

                var child = CrossFunction.Cross(father, mother, Constructor);
                newPopulation.Add(child);
            }

            // Randomly mutate some individuals
            Population = newPopulation.Select(individual => {
                // The MutatateFunction handles probability for us
                return MutateFunction.Mutate(individual, Constructor);
            }).ToList();
        }

        //Makes a population based on randomly generated points
        private void MakeInitialPopulation()
        {
            var random = new RandomDataStream();

            Population = new List<Individual>(PopulationSize);
            for (int i = 0; i < PopulationSize; i++)
            {
                var individual = Constructor.FromBinary(random);
                Population.Add(individual);
            }
        }
    }
}
