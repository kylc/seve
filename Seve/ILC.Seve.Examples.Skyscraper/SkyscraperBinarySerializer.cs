using System.IO;
using ILC.Seve.Genetics;
using ILC.Seve.Graph;

namespace ILC.Seve.Examples.Skyscraper
{
    public class SkyscraperBinarySerializer : IBinarySerializer
    {
        private readonly int VertexCount;
        private readonly long Max;

        public SkyscraperBinarySerializer(int vertexCount, long max)
        {
            this.VertexCount = vertexCount;
            this.Max = max;
        }

        public byte[] ToBinary(Individual _individual)
        {
            var individual = (SkyscraperIndividual)_individual;
            var graph = individual.Graph;

            var stream = new MemoryStream();

            using (stream)
            {
                var writer = new BinaryWriter(stream);

                // Write the length of the graph (int)
                writer.Write(graph.Vertices.Count);

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

        public Individual FromBinary(byte[] data)
        {
            var graph = new VertexGraph();
            var stream = new MemoryStream(data);

            using (stream)
            {
                var reader = new BinaryReader(stream);

                for (int i = 0; i < VertexCount; i++)
                {
                    var vertex = new Vertex(reader.ReadInt64(), reader.ReadInt64(), reader.ReadInt64(), Max);
                    graph.Vertices.Add(vertex);
                }

                var individual = new SkyscraperIndividual(graph);

                return individual;
            }
        }
    }
}
