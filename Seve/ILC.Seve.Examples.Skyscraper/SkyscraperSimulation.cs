using System.Collections.Generic;
using ILC.Seve.Genetics;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperSimulation
    {
        public const int PopulationSize = 20;
        public const int RunForGenerations = 100;

        // TODO: Make this the right constant.
        public const int CrossConstant = 50;
        public const int MutatePercent = 5;

        static void Main(string[] args)
        {
            // Create the initial population
            var individuals = new List<Individual>();
            for (int i = 0; i < PopulationSize; i++)
            {
                individuals.Add(new SkyscraperIndividual());
            }

            var crossFunction = new ConstantCrossFunction(CrossConstant);
            var mutateFunction = new ConstantMutateFunction(MutatePercent);
            var constructor = new SkyscraperBinarySerializer();

            var algorithm = new Algorithm(individuals, crossFunction, mutateFunction, constructor);
            var simulation = new SerialSimulation(algorithm);
            simulation.RunSimulation();
        }
    }
}
