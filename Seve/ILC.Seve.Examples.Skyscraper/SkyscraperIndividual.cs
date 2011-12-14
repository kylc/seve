using System;
using System.Linq;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperIndividual : Individual
    {
        public SkyscraperIndividual() : base()
        {
            // Add a bunch of random vertices to the graph
            var random = new Random();
            for (int i = 0; i < 20; i++)
            {
                var vertex = new Vertex(random.Next(50), random.Next(50), random.Next(50));
                Graph.Vertices.Add(vertex);
            }

            // For each node, get the three nearest nodes and link them up
            foreach (var node in Graph.Vertices)
            {
                var nearest = Graph.Vertices.OrderBy(a => node.DistanceTo(a)).Take(2);
                foreach (var other in nearest)
                {
                    node.ConnectTo(other);
                }
            }
        }

        public override Individual Cross(Individual b)
        {
            throw new NotImplementedException();
        }

        public override Individual Mutate()
        {
            throw new NotImplementedException();
        }

        public override int CalculateFitness(VertexGraph graph)
        {
            return (int) (graph.Vertices.OrderBy(a => a.Y).First().Y * 100);
        }
    }
}
