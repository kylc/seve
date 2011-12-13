using System;
using System.Collections.Generic;
using System.Linq;

namespace ILC.Seve.Genetics
{
    class Algorithm
    {
        public List<Individual> Population;

        public Algorithm(List<Individual> initialPopulation)
        {
            Population = initialPopulation;
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

                var child = father.Cross(mother);
                Population.Add(child);
            }

            // Randomly mutate some individuals
            var random = new Random();
            foreach (var individual in Population)
            {
                if (random.Next(100) < 5) // 5% chance of mutation (TODO: is this too high?)
                {
                    individual.Mutate();
                }
            }
        }
    }
}
