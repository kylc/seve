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

            var random = new Random();
            var targetSize = Population.Count;

            // Sort the population by the calculated fitnesses
            Population.Sort();

            // Select and breed the top portion of the population
            Population = Population.Take(Population.Count / 2).ToList();
            Population = Population.OrderBy(a => Guid.NewGuid()).ToList();

            while(Population.Count < targetSize)
            {
                var father = Population.ElementAt(random.Next(Population.Count));
                var mother = Population.ElementAt(random.Next(Population.Count));

                var child = CrossFunction.Cross(father, mother, Serializer);
                Population.Add(child);
            }

            // Randomly mutate some individuals
            Population.Select(individual => {
                // The MutatateFunction handles probability for us
                return MutateFunction.Mutate(individual, Serializer);
            });
        }
    }
}
