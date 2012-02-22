using System;
using System.IO;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperBinarySerializer : BinarySerializer
    {
        private readonly int VertexCount;
        private readonly long Max;

        public SkyscraperBinarySerializer(int vertexCount, long max)
        {
            this.VertexCount = vertexCount;
            this.Max = max;
        }

        public override byte[] ToBinary(Individual _individual)
        {
            var individual = (SkyscraperIndividual)_individual;
            var graph = individual.Graph;

            var stream = new MemoryStream();

            using (stream)
            {
                var writer = new BinaryWriter(stream);

                // Write the locations of each point on the graph (3 floats)
                foreach (var vertex in graph.Vertices)
                {
                    writer.Write(vertex.X);
                    writer.Write(vertex.Y);
                    writer.Write(vertex.Z);
                }
            }

            return stream.ToArray();
        }

        public override Individual FromBinary(Stream stream)
        {
            var graph = new VertexGraph();

            using (stream)
            {
                var reader = new BinaryReader(stream);

                for (int i = 0; i < VertexCount; i++)
                {
                    var vertex = new Vertex(ReadNormalLong(reader), ReadNormalLong(reader), ReadNormalLong(reader), Max);
                    graph.Vertices.Add(vertex);
                }

                var individual = new SkyscraperIndividual(graph);

                return individual;
            }
        }

        private long ReadNormalLong(BinaryReader s)
        {
            var value = s.ReadInt64();

            return Math.Abs(value);
        }
    }
}
