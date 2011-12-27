using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;
using ILC.Seve.Physics;

namespace ILC.Seve
{
    public class ParallelSimulation : ISimulation
    {
        private Algorithm Algorithm;

        public ParallelSimulation(Algorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public void RunSimulation()
        {
            Console.WriteLine("Running Simulation...");

            for (int i = 0; i < 100; i++)
            {
                var population = Algorithm.Population;

                Parallel.ForEach(population, individual =>
                {
                    Console.WriteLine("Testing individual {0}/{1} on thread {2}...",
                        population.IndexOf(individual) + 1, population.Count,
                        Thread.CurrentThread.ManagedThreadId);

                    var resultantGraph = RunPhysics(individual);
                    individual.Fitness = individual.CalculateFitness(resultantGraph);
                    Console.WriteLine(individual.Fitness);
                });

                Algorithm.Step();
            }
        }

        private VertexGraph RunPhysics(Individual individual)
        {
            var world = new DefaultSoftBodyWorld(individual.Graph);
            var physics = new PhysicsEngine(world);

            // Run for 30 seconds of simulated time
            // TODO: Increase this, it just makes it easier to test
            return physics.RunSimulation(30);
        }
    }
}
