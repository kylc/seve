using System;
using System.Collections.Generic;
using System.Threading;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;
using ILC.Seve.Physics;
using ILC.Seve.Web;
using System.Threading.Tasks;

namespace ILC.Seve
{
    class Simulation
    {
        private List<Individual> Population;

        public Simulation(List<Individual> population)
        {
            Population = population;
        }

        public void RunSimulation()
        {
            Console.WriteLine("Running Simulation...");

            Parallel.ForEach(Population, individual =>
            {
                Console.WriteLine("Testing individual {0}/{1} on thread {2}...", 
                    Population.IndexOf(individual) + 1, Population.Count,
                    Thread.CurrentThread.ManagedThreadId);

                var resultantGraph = RunPhysics(individual);
                individual.Fitness = individual.CalculateFitness(resultantGraph);
            });

            var algorithm = new Algorithm(Population);
            for (int i = 0; i < 100; i++)
            {
                algorithm.Step();
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
