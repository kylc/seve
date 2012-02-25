using System;
using System.Linq;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;
using ILC.Seve.Physics;

namespace ILC.Seve
{
    /// <summary>
    /// A simulation runner that tests individuals one by one.
    /// </summary>
    public class SerialSimulation : ISimulation
    {
        private Algorithm Algorithm;

        public SerialSimulation(Algorithm algorithm)
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
                    var resultantGraph = RunPhysics(individual);
                    individual.Graph = resultantGraph;

                    Console.WriteLine("Tested individual {0}/{1} = {2}",
                        population.IndexOf(individual) + 1, population.Count,
                        individual.Fitness);
                }

                Console.WriteLine("Average fitness of generation: {0}",
                    population.Select(a => a.Fitness).Average());

                Algorithm.Step();
            }
        }

        private VertexGraph RunPhysics(Individual individual)
        {
            var world = new DefaultRigidBodyWorld(individual.Graph);
            var physics = new PhysicsEngine(world);

            // Run for 30 seconds of simulated time
            // TODO: Increase this, it just makes it easier to test
            var graph = physics.RunSimulation(60);

            // TODO: Do we still need this for rigid bodies?
            // world.DisposeIndividual();

            return graph;
        }
    }
}
