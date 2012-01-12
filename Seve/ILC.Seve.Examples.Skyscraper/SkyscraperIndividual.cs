using System;
using System.Linq;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperIndividual : Individual
    {
        private static Random Random = new Random();

        public SkyscraperIndividual() : base()
        {
            // Add a bunch of random vertices to the graph
            for (int i = 0; i < 20; i++)
            {
                var vertex = new Vertex((byte)Random.Next(50), (byte)Random.Next(50), (byte)Random.Next(50));
                Graph.Vertices.Add(vertex);
            }
        }

        public SkyscraperIndividual(VertexGraph graph)
        {
            Graph = graph;
        }

        public override int CalculateFitness()
        {
            return (int) Graph.Vertices.OrderByDescending(a => a.Y).First().Y;
        }
    }
}
