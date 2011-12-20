using System;
using System.Collections.Generic;
using System.Linq;

namespace ILC.Seve.Genetics
{
    public class Algorithm
    {
        public List<Individual> Population;
        private ICrossFunction CrossFunction;
        private IMutateFunction MutateFunction;
        private IBinarySerializer Serializer;

        public Algorithm(List<Individual> initialPopulation, ICrossFunction crossFunction, IMutateFunction mutateFunction, IBinarySerializer serializer)
        {
            Population = initialPopulation;
            CrossFunction = crossFunction;
            MutateFunction = mutateFunction;
            Serializer = serializer;
        }

        public void Step()
        {
            // Fitness is assigned in Simulation class

            // Sort the population by the calculated fitnesses
            Population.Sort();

            // Select and breed the top portion of the population
            Population = Population.Take(Population.Count / 2).ToList();
            Population = Population.OrderBy(a => Guid.NewGuid()).ToList();

            for (int i = 0; i < Population.Count - 1; i += 2)
            {
                var father = Population.ElementAt(i);
                var mother = Population.ElementAt(i + 1);

                var child = CrossFunction.Cross(father, mother, Serializer);
                Population.Add(child);
            }

            // Randomly mutate some individuals
            var random = new Random();
            foreach (var individual in Population)
            {
                // The MutatateFunction handles probability for us
                MutateFunction.Mutate(individual, Serializer);
            }
        }
    }
}
