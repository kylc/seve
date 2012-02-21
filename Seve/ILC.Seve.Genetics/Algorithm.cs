﻿using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<Individual> Population;
        private ICrossFunction CrossFunction;
        private IMutateFunction MutateFunction;
        private BinarySerializer Serializer;

        public Algorithm(List<Individual> initialPopulation, ICrossFunction crossFunction, IMutateFunction mutateFunction, BinarySerializer serializer)
        {
            Population = initialPopulation;
            CrossFunction = crossFunction;
            MutateFunction = mutateFunction;
            Serializer = serializer;
        }

        /// <summary>
        /// Progress the population to the next generation.  This method
        /// handles all crossing and mutating.
        /// </summary>
        public void Step()
        {
            // Fitness is assigned in Simulation class

            var random = new Random();
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
                // TODO: Fix this.  Father could be the mother as well, also,
                // it's broken.
                var father = Population[random.Next(Population.Count)];
                var mother = Population[random.Next(Population.Count)];

                var child = CrossFunction.Cross(father, mother, Serializer);
                newPopulation.Add(child);
            }

            // Randomly mutate some individuals
            Population = newPopulation.Select(individual => {
                // The MutatateFunction handles probability for us
                return MutateFunction.Mutate(individual, Serializer);
            }).ToList();
        }
    }
}
