using System;
using System.Collections.Generic;
using ILC.Seve.Genetics;

namespace ILC.Seve.Examples.Skyscraper
{
    class SkyscraperSimulation
    {
        public const int PopulationSize = 20;
        public const int RunForGenerations = 100;

        static void Main(string[] args)
        {
            // Create the initial population
            var individuals = new List<Individual>();
            for (int i = 0; i < PopulationSize; i++)
            {
                individuals.Add(new SkyscraperIndividual());
            }

            var simulation = new Simulation(individuals);
            simulation.RunSimulation();
        }
    }
}
