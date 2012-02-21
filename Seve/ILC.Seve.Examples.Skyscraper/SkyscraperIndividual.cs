using System;
using System.Linq;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperIndividual : Individual
    {
        private static Random Random = new Random();

        public SkyscraperIndividual(int vertexCount, long max) : base()
        {
            // Add a bunch of random vertices to the graph
            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = new Vertex(Random.Next(50), Random.Next(100), Random.Next(50), max);
                Graph.Vertices.Add(vertex);
            }
        }

        public SkyscraperIndividual(VertexGraph graph)
        {
            Graph = graph;
        }

        public override int CalculateFitness()
        {
            return (int) Graph.Vertices.OrderByDescending(a => a.Y).First().ScaledY;
        }
    }
}
