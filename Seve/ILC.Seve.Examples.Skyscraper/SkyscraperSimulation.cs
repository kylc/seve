using System.Collections.Generic;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;
using System;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperSimulation
    {
        public const int PopulationSize = 20;
        public const int RunForGenerations = 100;

        // TODO: Make this the right constant.
        public const int CrossConstant = 50;
        public const int MutatePercent = 5;

        public const int VertexCount = 20;
        public const long Max = 3000;

        static void Main(string[] args)
        {
            var random = new Random();
            var constructor = new SkyscraperBinarySerializer(VertexCount, Max);

            // Create the initial population
            var individuals = new List<Individual>(PopulationSize);
            for (int i = 0; i < PopulationSize; i++)
            {
                // TODO: How about figuring out how much data we actually need?
                var randomData = new byte[1024];
                random.NextBytes(randomData);

                individuals.Add(constructor.FromBinary(randomData));
            }

            var crossFunction = new ConstantCrossFunction(CrossConstant);
            var mutateFunction = new ConstantMutateFunction(MutatePercent);

            var algorithm = new Algorithm(individuals, crossFunction, mutateFunction, constructor);
            var simulation = new SerialSimulation(algorithm);
            simulation.RunSimulation();
        }
    }
}
