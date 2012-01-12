using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;
using ILC.Seve.Physics;

namespace ILC.Seve
{
    /// <summary>
    /// A simulation runner that tests individuals in parallel, utilizing
    /// multiple CPU cores.
    /// </summary>
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

            // Each iteration is moving from one generation to the next
            for (int i = 0; i < 100; i++)
            {
                var population = Algorithm.Population;

                foreach(var individual in population)
                {
                    Console.WriteLine("Testing individual {0}/{1} on thread {2}...",
                        population.IndexOf(individual) + 1, population.Count,
                        Thread.CurrentThread.ManagedThreadId);

                    var resultantGraph = RunPhysics(individual);
                    individual.Graph = resultantGraph;

                }

                Console.WriteLine("Average fitness of generation: {0}",
                    population.Select(a => a.Fitness).Average());

                Algorithm.Step();
            }
        }

        private VertexGraph RunPhysics(Individual individual)
        {
            var world = new DefaultSoftBodyWorld(individual.Graph);
            var physics = new PhysicsEngine(world);

            // Run for 30 seconds of simulated time
            // TODO: Increase this, it just makes it easier to test
            var graph = physics.RunSimulation(60);

            world.DisposeIndividual();

            return graph;
        }
    }
}
