using System.Collections.Generic;
using ILC.Seve.Genetics;
using ILC.Seve.Util;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperSimulation
    {
        public const int PopulationSize = 20;
        public const int RunForGenerations = 100;

        // TODO: Make this the right constant.
        public const float CrossConstantRatio = 0.5F;
        public const int MutatePercent = 5;

        public const int VertexCount = 20;
        public const long Max = 3000;

        static void Main(string[] args)
        {
            var random = new RandomDataStream();
            var constructor = new SkyscraperBinarySerializer(VertexCount, Max);

            // Create the initial population
            var individuals = new List<Individual>(PopulationSize);
            for (int i = 0; i < PopulationSize; i++)
            {
                var individual = constructor.FromBinary(random);
                individuals.Add(individual);
            }

            var crossFunction = new ConstantRatioCrossFunction(CrossConstantRatio);
            var mutateFunction = new ConstantMutateFunction(MutatePercent);

            var algorithm = new Algorithm(individuals, crossFunction, mutateFunction, constructor);
            var simulation = new SerialSimulation(algorithm);
            simulation.RunSimulation();
        }
    }
}
